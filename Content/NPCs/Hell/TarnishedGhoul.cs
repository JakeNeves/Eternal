using Eternal.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Hell
{
    public class TarnishedGhoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarnished Ghoul");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 36;
            NPC.defense = 16;
            NPC.lifeMax = 1000;
            NPC.value = Item.sellPrice(silver: 6);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.DesertGhoul;
            AnimationType = NPCID.DesertGhoul;
            Banner = Item.NPCtoBanner(NPCID.DesertGhoul);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

                new FlavorTextBestiaryInfoElement("Something about these unfortunate ghouls is that they have taken things to the extreme, by having the top of their heads ripped off")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Making sure that the Tarnished Zombie ONLY spawn in Hell Mode
            if (DifficultySystem.hellMode)
                return SpawnCondition.OverworldDayDesert.Chance * 0.5f;
            else
                return SpawnCondition.OverworldDayDesert.Chance * 0f;
        }

    }
}
