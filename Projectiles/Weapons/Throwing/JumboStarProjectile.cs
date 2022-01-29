using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Throwing
{
    public class JumboStarProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jumbo Star");
        }

        public override void SetDefaults()
        {
            projectile.width = 186;
            projectile.height = 186;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 3;
            projectile.ranged = true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        private const float maxTicks = 45f;
        private const int alphaReducation = 25;

        public override void AI()
        {
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
                    float velXmult = 0.98f;
                    float velYmult = 0.35f;
                    projectile.ai[1] = maxTicks;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }

                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Shatter, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            Vector2 usePos = projectile.position;
            Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            Gore.NewGore(projectile.Center, projectile.velocity, mod.GetGoreSlot("Gores/JumboShard"), 1f);
            Gore.NewGore(projectile.Center, projectile.velocity, mod.GetGoreSlot("Gores/JumboShard"), 1f);
            Gore.NewGore(projectile.Center, projectile.velocity, mod.GetGoreSlot("Gores/JumboShard"), 1f);
            Gore.NewGore(projectile.Center, projectile.velocity, mod.GetGoreSlot("Gores/JumboShard"), 1f);
        }
    }
}
