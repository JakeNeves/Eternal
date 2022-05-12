using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class TheTrinityProjectileIce : ModProjectile
    {

        //it now goes into orbit lul...

        public override void SetStaticDefaults()
        {
            // ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            // ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 575f;
            // ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 25f;

            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.height = 18;
            projectile.width = 18;

            projectile.penetrate = -1;
            projectile.scale = 1f;

            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile proj = Main.projectile[(int)projectile.localAI[0]];

            projectile.timeLeft++;
            projectile.rotation += 0.1f;

            if (!proj.active || proj.type != ModContent.ProjectileType<TheTrinityBaseProjectile>() || proj.owner != projectile.owner)
            {
                projectile.Kill();
                return;
            }

            if (projectile.owner == Main.myPlayer)
            {
                //rotating mf
                float distanceFromPlayer = 48;

                projectile.position = proj.Center + new Vector2(distanceFromPlayer, 0f).RotatedBy(projectile.ai[1]);
                projectile.position.X -= projectile.width / 2;
                projectile.position.Y -= projectile.height / 2;

                float rotation = (float)Math.PI / 75;
                projectile.ai[1] += rotation;
                if (projectile.ai[1] > (float)Math.PI)
                {
                    projectile.ai[1] -= 2f * (float)Math.PI;
                    projectile.netUpdate = true;
                }
            }

            projectile.damage = proj.damage;
            projectile.knockBack = proj.knockBack;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 120);
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
