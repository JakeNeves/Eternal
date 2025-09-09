using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class CarminiteSludge2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(in SoundID.NPCHit1, Projectile.position);
            }
            else if (Projectile.ai[1] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num2 = -1;
                float num3 = 1500f;
                for (int k = 0; k < 255; k++)
                {
                    if (Main.player[k].active && !Main.player[k].dead)
                    {
                        Vector2 center = Main.player[k].Center;
                        float num4 = Vector2.Distance(center, Projectile.Center);
                        if ((num4 < num3 || num2 == -1) && Collision.CanHit(Projectile.Center, 1, 1, center, 1, 1))
                        {
                            num3 = num4;
                            num2 = k;
                        }
                    }
                }
                if (num3 < 20f)
                {
                    Projectile.Kill();
                    return;
                }
                if (num2 != -1)
                {
                    Projectile.ai[1] = 21f;
                    Projectile.ai[0] = num2;
                    Projectile.netUpdate = true;
                }
            }
            else if (Projectile.ai[1] > 20f && Projectile.ai[1] < 200f)
            {
                Projectile.ai[1] += 1f;
                int num5 = (int)Projectile.ai[0];
                if (!Main.player[num5].active || Main.player[num5].dead)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                else
                {
                    float num6 = Projectile.velocity.ToRotation();
                    Vector2 vector = Main.player[num5].Center - Projectile.Center;
                    if (vector.Length() < 20f)
                    {
                        Projectile.Kill();
                        return;
                    }
                    float targetAngle = vector.ToRotation();
                    if (vector == Vector2.Zero)
                    {
                        targetAngle = num6;
                    }
                    float num7 = num6.AngleLerp(targetAngle, 0.008f);
                    Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(num7);
                }
            }
            if (Projectile.ai[1] >= 1f && Projectile.ai[1] < 20f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] == 20f)
                {
                    Projectile.ai[1] = 1f;
                }
            }
            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
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
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
            SoundEngine.PlaySound(SoundID.Item111, Projectile.Center);
        }

    }
}
