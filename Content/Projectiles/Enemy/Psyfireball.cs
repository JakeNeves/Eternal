using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Enemy
{
    public class Psyfireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.timeLeft = 0;
            Projectile.Kill();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void AI()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, Projectile.position);
            }
            else if (Projectile.ai[1] == 1f && Main.netMode != 1)
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
            Lighting.AddLight(Projectile.Center, 1.27f, 0.22f, 0.76f);
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] == 12f)
            {
                Projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l++)
                {
                    Vector2 spinningpoint = Vector2.UnitX * -Projectile.width / 2f;
                    spinningpoint += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
                    spinningpoint = spinningpoint.RotatedBy(Projectile.rotation - (float)Math.PI / 2f);
                    int num8 = Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleTorch, 0f, 0f, 160);
                    Main.dust[num8].scale = 1.1f;
                    Main.dust[num8].noGravity = true;
                    Main.dust[num8].position = Projectile.Center + spinningpoint;
                    Main.dust[num8].velocity = Projectile.velocity * 0.1f;
                    Main.dust[num8].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
                }
            }
            if (Main.rand.NextBool(4))
            {
                for (int m = 0; m < 1; m++)
                {
                    Vector2 vector2 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(Projectile.velocity.ToRotation());
                    int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100);
                    Main.dust[num9].velocity *= 0.1f;
                    Main.dust[num9].position = Projectile.Center + vector2 * Projectile.width / 2f;
                    Main.dust[num9].fadeIn = 0.9f;
                }
            }
            if (Main.rand.NextBool(32))
            {
                for (int n = 0; n < 1; n++)
                {
                    Vector2 vector3 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy(Projectile.velocity.ToRotation());
                    int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 155, default(Color), 0.8f);
                    Main.dust[num10].velocity *= 0.3f;
                    Main.dust[num10].position = Projectile.Center + vector3 * Projectile.width / 2f;
                    if (Main.rand.NextBool(2))
                    {
                        Main.dust[num10].fadeIn = 1.4f;
                    }
                }
            }
            if (Main.rand.NextBool(2))
            {
                for (int num11 = 0; num11 < 2; num11++)
                {
                    Vector2 vector4 = -Vector2.UnitX.RotatedByRandom(0.7853981852531433).RotatedBy(Projectile.velocity.ToRotation());
                    int num12 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 0, default(Color), 1.2f);
                    Main.dust[num12].velocity *= 0.3f;
                    Main.dust[num12].noGravity = true;
                    Main.dust[num12].position = Projectile.Center + vector4 * Projectile.width / 2f;
                    if (Main.rand.NextBool(2))
                    {
                        Main.dust[num12].fadeIn = 1.4f;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            for (int i = 0; i < 5; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int i = 0; i < 5; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DemonTorch, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
        }
    }
}
