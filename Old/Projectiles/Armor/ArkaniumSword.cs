﻿using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Armor
{
    public class ArkaniumSword : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 46;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 150;
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

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void Kill(int timeLeft)
        {
            //Main.PlaySound(SoundID.Tink, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            Main.PlaySound(SoundID.DD2_DarkMageHealImpact, projectile.position);
        }

        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<ArkEnergy>(), projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

    }
}