using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Gehenna
{
    public class CoagulatedBloodslime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 64;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            if (Main.hardMode)
            {
                NPC.damage = 10;
                NPC.defense = 15;
                NPC.lifeMax = 100;
            }
            else
            {
                NPC.damage = 20;
                NPC.defense = 30;
                NPC.lifeMax = 250;
            }
            AnimationType = NPCID.BlueSlime;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new FlavorTextBestiaryInfoElement("Normally you wouldn't find these elsewhere except for the Gehenna, where they are just made up of a bunch of blood!")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isHardmodeRule = new(new Conditions.IsHardmode());

            isHardmodeRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CoagulatedBlood>(), 1, 4, 6));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna && Main.hardMode)
                return SpawnCondition.Underworld.Chance * 0.5f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
        }
    }
}
