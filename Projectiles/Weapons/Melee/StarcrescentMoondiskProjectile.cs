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
        int timer;

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

        public override void AI()
        {
            timer++;
            switch (timer)
            {
                case 10:
                    Projectile.NewProjectile(projectile.position.X + 20, projectile.position.Y + 20, projectile.direction, 0, ProjectileType<StarcrescentProjectile>(), 30, 0f, Main.myPlayer, 0f, 0f);
                    break;
                case 40:
                    timer = 0;
                    break;
            }

            Lighting.AddLight(projectile.position, 0.191f, 0.23f, 0.23f);
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
