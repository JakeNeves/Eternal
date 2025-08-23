using Eternal.Content.NPCs.Misc;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class UmbralArcanisProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 200;
            Projectile.extraUpdates = 2;
            Projectile.alpha = 255;
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            if (Main.rand.NextBool(6))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    NPC.NewNPC(entitySource, (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<BenightedWisp>());
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    SoundEngine.PlaySound(SoundID.Item104, Projectile.position);

                if (Main.rand.NextBool(2))
                    Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
                Projectile.alpha -= 5;
            else
                Projectile.alpha = 0;

            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            if (!Main.dedServ)
                Lighting.AddLight(Projectile.Center, 0.75f, 0f, 0.75f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);

                Projectile.frame = Main.rand.Next(0, 3);
                Projectile.ai[0] = 1f;
            }
        }
    }
}
