using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Carrion
{
    public class Level2Ghoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 36;
            NPC.defense = 16;
            NPC.lifeMax = 150;
            NPC.value = Item.sellPrice(silver: 6);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.DesertGhoul;
            AnimationType = NPCID.DesertGhoul;
            Banner = Item.NPCtoBanner(NPCID.DesertGhoul);
            BannerItem = Item.BannerToItem(Banner);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.CarrionDesertSurface>().Type, ModContent.GetInstance<Biomes.UndergroundCarrion>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new FlavorTextBestiaryInfoElement("These Ghouls were given a second chance at life...")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneCarrion)
                return SpawnCondition.OverworldDayDesert.Chance * 0.5f + SpawnCondition.DesertCave.Chance * 0f;
            else if (ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion)
                return SpawnCondition.OverworldDayDesert.Chance * 0f + SpawnCondition.DesertCave.Chance * 0.5f;
            else
                return SpawnCondition.OverworldDayDesert.Chance * 0f + SpawnCondition.DesertCave.Chance * 0f;
        }
    }
}
