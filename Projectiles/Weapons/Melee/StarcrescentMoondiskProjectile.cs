using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class StarcrescentMoondiskProjectile : ModProjectile
    {
        //int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starcrescent Moondisk");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.melee= true;
            projectile.penetrate = 3;
            projectile.timeLeft = 600;
            projectile.light = 1.0f;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Electric, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }

            if (Main.rand.Next(1) == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8, -8, ProjectileType<StarcrescentProjectile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8, -8, ProjectileType<StarcrescentProjectile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8, 8, ProjectileType<StarcrescentProjectile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8, 8, ProjectileType<StarcrescentProjectile>(), 6, 0, Main.myPlayer, 0f, 0f);
            }
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
