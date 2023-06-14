using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.NeoxMechs
{
    public class PolarusLaserTurret : ModNPC
    {
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        int attackTimer = 0;

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
            return ModContent.NPCType<PolarusShield>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Polarus' Shield Laser Turret");
        }

        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lifeMax = 4000;
            NPC.defense = 40;
            NPC.width = 38;
            NPC.height = 72;
            NPC.damage = 60;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
        }

        public override void AI()
        {
            if (Despawn())
            {
                return;
            }

            MoveInFormation();

            attackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Player target = Main.player[NPC.target];

            Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

            if (attackTimer == 100)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                attackTimer = 0;
            }
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

            float rad = (float)PositionIndex / PolarusShield.TurretCount() * MathHelper.TwoPi;

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

            float speed = 48f;
            float inertia = 20;

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;

            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
    }
}
