using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Dusts;
using System;
using Terraria.ID;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class CosmicFistProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Fist");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 25;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 500;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Starmetal>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void AI()
        {

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            if (Main.myPlayer == projectile.owner && projectile.ai[0] == 0f)
            {

                Player player = Main.player[projectile.owner];
                if (player.channel)
                {
                    float maxDistance = 24f;
                    Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    if (distanceToCursor > maxDistance)
                    {
                        distanceToCursor = maxDistance / distanceToCursor;
                        vectorToCursor *= distanceToCursor;
                    }

                    int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
                    int oldVelocityXBy1000 = (int)(projectile.velocity.X * 1000f);
                    int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
                    int oldVelocityYBy1000 = (int)(projectile.velocity.Y * 1000f);

                    if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                    {
                        projectile.netUpdate = true;
                    }

                    projectile.velocity = vectorToCursor;

                }
            }

            else if (projectile.ai[0] == 0f)
            {

                Lighting.AddLight(projectile.position, 0.75f, 0f, 0.75f);

                projectile.spriteDirection = projectile.direction;

                projectile.netUpdate = true;

                float maxDistance = 12f;
                Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
                float distanceToCursor = vectorToCursor.Length();

                if (distanceToCursor == 0f)
                {
                    vectorToCursor = projectile.Center - projectile.Center;
                    distanceToCursor = vectorToCursor.Length();
                }

                distanceToCursor = maxDistance / distanceToCursor;
                vectorToCursor *= distanceToCursor;

                projectile.velocity = vectorToCursor;

                if (projectile.velocity == Vector2.Zero)
                {
                    projectile.Kill();
                }

                projectile.ai[0] = 1f;
            }

            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
            }

        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Starmetal>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }

            if (projectile.penetrate == 1)
            {
                projectile.maxPenetrate = -1;
                projectile.penetrate = -1;

                int explosionArea = 60;
                Vector2 oldSize = projectile.Size;
                projectile.position = projectile.Center;
                projectile.Size += new Vector2(explosionArea);
                projectile.Center = projectile.position;

                projectile.tileCollide = false;
                projectile.velocity *= 0.01f;
                projectile.Damage();
                projectile.scale = 0.01f;

                projectile.position = projectile.Center;
                projectile.Size = new Vector2(10);
                projectile.Center = projectile.position;
            }
        }
    }
}
