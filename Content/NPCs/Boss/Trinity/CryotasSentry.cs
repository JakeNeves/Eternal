using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    public class CryotasSentry : ModNPC
    {
        private Player player;

        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        bool iceBombTrail = false;

        int attackTimer = 0;
        int iceBombTimer = 0;

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;

        public const float RotationTimerMax = 360;
        public ref float RotationTimer => ref NPC.ai[2];

        public static int BodyType()
        {
            return ModContent.NPCType<Cryota>();
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lifeMax = 8000;
            NPC.defense = 95;
            NPC.width = 46;
            NPC.height = 56;
            NPC.damage = 80;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
        }

        public override void AI()
        {
            if (Despawn())
            {
                return;
            }

            Target();
            RotateNPCToTarget();
            MoveInFormation();

            attackTimer++;

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            var entitySource = NPC.GetSource_FromAI();

            if (iceBombTrail)
            {
                iceBombTimer++;

                if (iceBombTimer > 10)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DetonatingIceBomb>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                    iceBombTimer = 0;
                }
            }
            else
            {
                iceBombTimer = 0;
            }

            if (attackTimer == 200)
            {
                iceBombTrail = true;
            }
            if (attackTimer == 400)
            {
                iceBombTrail = false;
            }
            if (attackTimer == 400)
            {
                attackTimer = 0;
            }
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            SoundEngine.PlaySound(SoundID.Tink, NPC.position);
            if (NPC.life <= 0)
            {
                _ = NPC.Center;
                for (int num120 = 0; num120 < 60; num120++)
                {
                    int num121 = 25;
                    _ = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                    int num122 = Dust.NewDust(NPC.Center - Vector2.One * num121, num121 * 2, num121 * 2, DustID.Ice);
                    Dust dust154 = Main.dust[num122];
                    Vector2 vector7 = Vector2.Normalize(dust154.position - NPC.Center);
                    dust154.position = NPC.Center + vector7 * 25f * NPC.scale;
                    if (num120 < 30)
                    {
                        dust154.velocity = vector7 * dust154.velocity.Length();
                    }
                    else
                    {
                        dust154.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust154.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
                    dust154.color = Color.Lerp(dust154.color, Color.White, 0.3f);
                    dust154.noGravity = true;
                    dust154.scale = 0.7f;
                }
            }
        }

        private void Target()
        {
            player = Main.player[NPC.target];
        }

        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient &&
                (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType()))
            {
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                return true;
            }
            return false;
        }

        private void MoveInFormation()
        {
            NPC parentNPC = Main.npc[ParentIndex];

            float rad = (float)PositionIndex / Cryota.SentryCount() * MathHelper.TwoPi;

            RotationTimer += 0.15f;
            if (RotationTimer > RotationTimerMax)
            {
                RotationTimer = 0;
            }

            float continuousRotation = MathHelper.ToRadians(RotationTimer);
            rad += continuousRotation;
            if (rad > MathHelper.TwoPi)
            {
                rad -= MathHelper.TwoPi;
            }
            else if (rad < 0)
            {
                rad += MathHelper.TwoPi;
            }

            float distanceFromBody = parentNPC.width + NPC.width;

            Vector2 offset = Vector2.One.RotatedBy(rad) * distanceFromBody;

            Vector2 destination = parentNPC.Center + offset;
            Vector2 toDestination = destination - NPC.Center;
            Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.Zero);

            float speed = 45f;
            float inertia = 15;

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }
    }
}
