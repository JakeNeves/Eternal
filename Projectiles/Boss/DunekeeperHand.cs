using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Eternal.Projectiles.Boss
{
    public class DunekeeperHand : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 38;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 250;
        }

        private const float maxTicks = 80f;
        private const int alphaReducation = 25;

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            projectile.alpha += 1;

            if (projectile.alpha > 0)
            {
                projectile.alpha -= alphaReducation;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= maxTicks)
                {
                    projectile.alpha -= 10;

                    /*float velXmult = 0.0f;
                    float velYmult = 0.0f;
                    projectile.ai[1] = maxTicks;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;*/
                }

                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            }
        }

        public override void Kill(int timeLeft)
        {
            projectile.alpha = 255;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Tungsten, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

    }
}
