using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Joke
{
    public class BigHeadSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 74;
            NPC.damage = 40;
            NPC.defense = 12;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 6);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 3;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            Banner = Item.NPCtoBanner(NPCID.Skeleton);
            BannerItem = Item.BannerToItem(Banner);
        }
    }
}
