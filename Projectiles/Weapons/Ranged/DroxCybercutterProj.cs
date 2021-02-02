using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.Projectiles.Weapons.Ranged
{
    public class DroxCybercutterProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drox Cybercutter");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.light = 0.8f;
            projectile.friendly = true;
            projectile.ranged = true;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(191, 23, 23, 0);

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.191f, 0.23f, 0.23f);
            projectile.spriteDirection = projectile.direction;
            projectile.rotation += projectile.velocity.X * 0.1f;
            if (projectile.soundDelay == 0 && Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) > 2f)
            {
                projectile.soundDelay = 10;
                Main.PlaySound(SoundID.Item15, projectile.position);
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

            Main.PlaySound(SoundID.Item15, projectile.position);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

    }
}
