using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Eternal.Common.Configurations;

namespace Eternal.Content.NPCs.Carrion
{
    public class Polyp : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 32;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCDeath22;
            NPC.DeathSound = SoundID.NPCDeath12;
            AIType = NPCID.BlueSlime;
            NPC.damage = 15;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            AnimationType = NPCID.BlueSlime;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.CarrionSurface>().Type, ModContent.GetInstance<Biomes.UndergroundCarrion>().Type ];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenBlood, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Who said they can't count as slimes, despite being a heaping clump of necrotic tissue...")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PolypChunk>(), 1, 3, 5));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneCarrion)
                return SpawnCondition.OverworldDay.Chance * 0.5f + SpawnCondition.Underground.Chance * 0f;
            else if (ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion)
                return SpawnCondition.OverworldDay.Chance * 0f + SpawnCondition.Underground.Chance * 0.5f;
            else
                return SpawnCondition.OverworldDay.Chance * 0f + SpawnCondition.Underground.Chance * 0f;
        }
    }
}
