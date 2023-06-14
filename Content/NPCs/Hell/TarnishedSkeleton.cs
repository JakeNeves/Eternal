using Eternal.Common.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Hell
{
    public class TarnishedSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tarnished Skeleton");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 40;
            NPC.defense = 12;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 6);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            Banner = Item.NPCtoBanner(NPCID.Skeleton);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("You thought these skeletons should be dead, think again...")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Making sure that the Tarnished Zombie ONLY spawn in Hell Mode
            if (DifficultySystem.hellMode)
                return SpawnCondition.Underground.Chance * 0.5f;
            else
                return SpawnCondition.Underground.Chance * 0f;
        }

    }
}
