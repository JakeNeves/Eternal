using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class CosmigeddonBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 94;
            Projectile.height = 94;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
            Projectile.alpha = 0;
        }

        private const float maxTicks = 80f;
        private const int alphaReducation = 25;

        public override void AI()
        {
            Projectile.rotation += 0.15f;
            Projectile.alpha += 15;

            Lighting.AddLight(Projectile.position, 1.98f, 0.49f, 2.47f);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= alphaReducation;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= maxTicks)
                {
                    Projectile.alpha -= 10;
                }

                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            }

            float dustScale = 0.5f;
            if (Projectile.ai[0] == 0f)
                dustScale = 0.05f;
            else if (Projectile.ai[0] == 1f)
                dustScale = 0.10f;
            else if (Projectile.ai[0] == 2f)
                dustScale = 0.15f;

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
                if (Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                }

                dust.scale *= 0.5f;
                dust.velocity *= 0.2f;
                dust.scale *= dustScale;
            }
            Projectile.ai[0] += 1f;

            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();
            Projectile.alpha = 255;

            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            for (int i = 0; i < Main.rand.Next(3, 4); i++)
            {
                Projectile.NewProjectile(entitySource, (int)Projectile.position.X, (int)Projectile.position.Y, Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4), ModContent.ProjectileType<CosmicFireball>(), 60, 0);
            }
        }
    }
}
