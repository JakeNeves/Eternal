using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Explosion
{
    public class PussBurst : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.timeLeft = 1;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Puss>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

        public override void OnKill(int timeLeft)
        {
            _ = Projectile.Center;
            for (int num120 = 0; num120 < 60; num120++)
            {
                int num121 = 25;
                _ = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                int num122 = Dust.NewDust(Projectile.Center - Vector2.One * num121, num121 * 2, num121 * 2, ModContent.DustType<Puss>());
                Dust dust154 = Main.dust[num122];
                Vector2 vector7 = Vector2.Normalize(dust154.position - Projectile.Center);
                dust154.position = Projectile.Center + vector7 * 25f * Projectile.scale;
                if (num120 < 30)
                {
                    dust154.velocity = vector7 * dust154.velocity.Length();
                }
                else
                {
                    dust154.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                }
                dust154.noGravity = true;
                dust154.scale = 0.7f;
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Puss>(), Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }

            SoundEngine.PlaySound(SoundID.NPCDeath22, Projectile.Center);
        }
    }
}
