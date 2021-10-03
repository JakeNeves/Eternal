using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Items.Materials;
using Eternal.Buffs;
using Eternal.Items.Potions;
using Eternal.Items.Tools;
using Eternal.Projectiles.Boss;
using Eternal.Dusts;

namespace Eternal.Projectiles.Boss
{
    public class EmperorBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor's Impact Blade");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 18;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 38;
            projectile.height = 94;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(mod.GetTexture("Projectiles/Boss/EmperorBlade_Shadow"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.1f;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<EmperorFire>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }

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

    }
}
