using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Armor
{
    public class ArkaniumSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
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
            SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, Projectile.position);
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.JungleTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                    Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.GreenTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                Projectile.ai[0] = 1f;
            }

            if (Main.rand.NextBool(4))
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}
