using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class BorealisNeoxSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXR-2303 Borealis-N30X Armor Penetrating Spike");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 22;
            projectile.height = 52;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.RedFracture>(), 4096);
        }

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            projectile.alpha -= 1;

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 60f)
                {
                    float velXmult = 0.90f;
                    float velYmult = 0.80f;
                    projectile.ai[1] = 60f;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }
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

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath14, projectile.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }

    }
}
