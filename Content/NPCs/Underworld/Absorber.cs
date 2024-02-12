using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Misc;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Underworld
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
            NPC.knockBackResist = -1f;
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
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

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
            if (!DownedBossSystem.downedIncinerius)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.5f;
        }
    }
}
