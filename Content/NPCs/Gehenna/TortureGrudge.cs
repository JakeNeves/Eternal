using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Eternal.Content.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Gehenna
{
    public class TortureGrudge : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 2000;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = 21;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("These rolling constructs can cling to walls and climb up and down and stick to ceilings, just like their Mausoleum counterpart!")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isHardmodeRule = new(new Conditions.IsHardmode());

            isHardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValtoricKnives>(), 24));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (DownedBossSystem.downedIncinerius && ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                return SpawnCondition.Underworld.Chance * 0.5f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.RedTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.RedTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
