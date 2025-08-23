using Eternal.Content.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Misc
{
    public class BenightedWisp : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.aiStyle = -1;
            NPC.width = 22;
            NPC.height = 38;
            NPC.npcSlots = 0;
            NPC.HitSound = null;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.noGravity = true;
            NPC.dontTakeDamageFromHostiles = true;
            NPCID.Sets.ProjectileNPC[NPC.type] = true;
        }

        public override void AI()
        {
            if (NPC.alpha > 0)
                NPC.alpha -= 5;
            else
                NPC.alpha = 0;

            Lighting.AddLight(NPC.position, 1.27f, 0.22f, 0.76f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 4f, 0f, ModContent.ProjectileType<BenightedWispProjectile>(), 60, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 4f, ModContent.ProjectileType<BenightedWispProjectile>(), 60, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -4f, 0f, ModContent.ProjectileType<BenightedWispProjectile>(), 60, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -4f, ModContent.ProjectileType<BenightedWispProjectile>(), 60, 0);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        public override bool CheckDead() => false;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
