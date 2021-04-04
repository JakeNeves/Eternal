using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles.Enemy
{
    public class ArkArrowHostile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 46;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        private const float maxTicks = 20f;
        private const int alphaReducation = 25;

        public override void Kill(int timeLeft)
        {
            //Main.PlaySound(SoundID.Tink, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            Main.PlaySound(SoundID.NPCHit4, Main.myPlayer);
            Vector2 usePos = projectile.position;
            Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
        }

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

    }
}
