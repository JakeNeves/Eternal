﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class CosmicFireball : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 60;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
            projectile.light = 0.75f;
        }

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            Lighting.AddLight(projectile.position, 1.98f, 0.49f, 2.47f);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }

            if (projectile.localAI[0] == 0f)
            {
                int whateverThisIntIs = 0;
                while ((float)whateverThisIntIs < 16f)
                {
                    Vector2 vector2 = Vector2.UnitX * 0f;
                    vector2 += -Vector2.UnitY.RotatedBy((double)((float)whateverThisIntIs * (6.28318548f / 16f)), default(Vector2)) * new Vector2(1f, 4f);
                    vector2 = vector2.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    whateverThisIntIs++;
                }
                projectile.localAI[0] = 1f;
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
            projectile.alpha = 255;
            Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.position);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

    }
}