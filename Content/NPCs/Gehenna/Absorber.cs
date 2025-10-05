using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.NPCs.Gehenna
{
    public class Absorber : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 100;
            NPC.defense = 90;
            NPC.lifeMax = 1000;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.PossessedArmor;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.rarity = 4;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new FlavorTextBestiaryInfoElement("These basalt constructs can absorb almost every attack, they originally worked in the Gehanna's forges before being punished by Incinerius for insufficent formation of basalt.")
            });
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isHardmodeRule = new(new Conditions.IsHardmode());

            isHardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HellHacker>(), 24));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GehennaKeyFragment2>(), 12));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, -1f, 0, default(Color), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedIncinerius && ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                return SpawnCondition.Underworld.Chance * 0.01f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
        }
    }
}
