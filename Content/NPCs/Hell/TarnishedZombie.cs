using Eternal.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Hell
{
    public class TarnishedZombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tarnished Zombie");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 28;
            NPC.defense = 6;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 6);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            Banner = Item.NPCtoBanner(NPCID.Zombie);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("They were once innocent beings, now they have been brutally tarnished...")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Making sure that the Tarnished Zombie ONLY spawn in Hell Mode
            if (DifficultySystem.hellMode)
                return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
        }

    }
}
