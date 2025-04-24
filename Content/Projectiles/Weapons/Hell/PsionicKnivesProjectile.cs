using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Hell
{
    public class PsionicKnivesProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            Projectile.timeLeft = 0;
            Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

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
            Lighting.AddLight(Projectile.position, 1.27f, 0.22f, 0.76f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
            {
                Projectile.soundDelay = 10;
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
                {
                    Volume = 0.05f,
                    PitchVariance = Main.rand.NextFloat(1.2f, 1.9f),
                    MaxInstances = 0,
                }, Projectile.position);
            }

            Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5));
            Dust dust = Dust.NewDustPerfect(dustPosition, DustID.DemonTorch, null, 100, Color.Lime, 0.8f);
            dust.velocity *= 0.3f;
            dust.noGravity = true;

            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
            {
                Player player = Main.player[Projectile.owner];
                if (player.channel)
                {
                    float maxDistance = 18f;
                    Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    if (distanceToCursor > maxDistance)
                    {
                        distanceToCursor = maxDistance / distanceToCursor;
                        vectorToCursor *= distanceToCursor;
                    }

                    int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
                    int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 1000f);
                    int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
                    int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 1000f);

                    if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                    {
                        Projectile.netUpdate = true;
                    }

                    Projectile.velocity = vectorToCursor;

                }
                else if (Projectile.ai[0] == 0f)
                {
                    Projectile.netUpdate = true;

                    float maxDistance = 14f;
                    Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    if (distanceToCursor == 0f)
                    {
                        vectorToCursor = Projectile.Center - player.Center;
                        distanceToCursor = vectorToCursor.Length();
                    }

                    distanceToCursor = maxDistance / distanceToCursor;
                    vectorToCursor *= distanceToCursor;

                    Projectile.velocity = vectorToCursor;

                    if (Projectile.velocity == Vector2.Zero)
                    {
                        Projectile.Kill();
                    }

                    Projectile.ai[0] = 1f;
                }
            }

            float maxDetectRadius = 100f;
            float projSpeed = 12f;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
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
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            if (Projectile.penetrate == 1)
            {
                Projectile.maxPenetrate = -1;
                Projectile.penetrate = -1;

                int explosionArea = 60;
                Vector2 oldSize = Projectile.Size;
                Projectile.position = Projectile.Center;
                Projectile.Size += new Vector2(explosionArea);
                Projectile.Center = Projectile.position;

                Projectile.tileCollide = false;
                Projectile.velocity *= 0.01f;
                Projectile.Damage();
                Projectile.scale = 0.01f;

                Projectile.position = Projectile.Center;
                Projectile.Size = new Vector2(10);
                Projectile.Center = Projectile.position;
            }
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
    }
}
