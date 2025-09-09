using Eternal.Content.Buffs.Minions;
using Eternal.Content.Projectiles.Weapons.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Minions
{
    public class RottenMindSentry : ModProjectile
    {
        public ref float ShootTimer => ref Projectile.ai[0];

        public bool Floating => Projectile.ai[2] == 0;

        public bool JustSpawned
        {
            get => Projectile.localAI[0] == 0;
            set => Projectile.localAI[0] = value ? 0 : 1;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 98;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.ignoreWater = true;
            Projectile.netImportant = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            const int ShootFrequency = 15;
            const int TargetingRange = 50 * 16;
            const float FireVelocity = 10f;

            if (JustSpawned)
            {
                JustSpawned = false;
                ShootTimer = ShootFrequency * 1.5f;

                SoundEngine.PlaySound(SoundID.Item46, Projectile.position);

                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GreenBlood, speed * 4, Scale: 1.5f);
                    d.noGravity = true;
                }
            }

            Projectile.velocity.X = 0f;
            if (!Floating)
            {
                Projectile.velocity.Y += 0.2f;
                if (Projectile.velocity.Y > 16f)
                {
                    Projectile.velocity.Y = 16f;
                }
            }

            float closestTargetDistance = TargetingRange;
            NPC targetNPC = null;
            if (Projectile.OwnerMinionAttackTargetNPC != null)
            {
                TryTargeting(Projectile.OwnerMinionAttackTargetNPC, ref closestTargetDistance, ref targetNPC);
            }

            if (targetNPC == null)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    TryTargeting(npc, ref closestTargetDistance, ref targetNPC);
                }
            }

            if (targetNPC != null)
            {
                if (ShootTimer <= 0)
                {
                    ShootTimer = ShootFrequency;

                    SoundEngine.PlaySound(SoundID.NPCHit1 with { Volume = 0.4f }, Projectile.Center);

                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 shootDirection = (targetNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
                        Vector2 shootVelocity = shootDirection * FireVelocity;

                        int type = ModContent.ProjectileType<RottenMindTomaBall>();

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X - 4f, Projectile.Center.Y), shootVelocity, type, Projectile.damage, 3, Projectile.owner);
                    }
                }
            }

            ShootTimer--;

            if (ShootTimer > ShootFrequency)
            {
                Projectile.frame = 0;
            }
            else if (targetNPC == null)
            {
                if (++Projectile.frameCounter >= 60)
                {
                    Projectile.frameCounter = 0;
                }
                Projectile.frame = Projectile.frameCounter < 30 ? 1 : 2;
            }
            else
            {
                Projectile.frame = 3;
            }
        }

        private void TryTargeting(NPC npc, ref float closestTargetDistance, ref NPC targetNPC)
        {
            if (npc.CanBeChasedBy(this))
            {
                float distanceToTargetNPC = Vector2.Distance(Projectile.Center, npc.Center);
                if (distanceToTargetNPC < closestTargetDistance && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    closestTargetDistance = distanceToTargetNPC;
                    targetNPC = npc;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);

            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 50, DustID.GreenBlood, speed * -5, Scale: 1.5f);
                d.noGravity = true;
            }
        }
    }
}
