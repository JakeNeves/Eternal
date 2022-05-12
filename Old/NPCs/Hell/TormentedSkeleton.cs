using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Hell
{
    public class TormentedSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tormented Skeleton");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Skeleton];
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 54;
            npc.damage = 24;
            npc.defense = 12;
            npc.lifeMax = 460;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = Item.sellPrice(gold: 1, silver: 3);
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 3;
            aiType = NPCID.Skeleton;
            animationType = NPCID.Skeleton;
            banner = Item.NPCtoBanner(NPCID.Skeleton);
            bannerItem = Item.BannerToItem(banner);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Making sure that the Tormented Skeleton ONLY spawn in Hell Mode
            if (EternalWorld.hellMode && EternalWorld.downedCarmaniteScouter)
                return SpawnCondition.Underground.Chance * 0.8f;
            else
                return SpawnCondition.Underground.Chance * 0f;
        }

    }
}
