using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.NeoxMechs
{
    public class PolarusShield : ModNPC
    {
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool SpawnedTurrets
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public static int TurretCount()
        {
            int count = 3;

            return count;
        }

        private void SpawnTurrets()
        {
            if (SpawnedTurrets)
            {
                return;
            }

            SpawnedTurrets = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int count = TurretCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<PolarusLaserTurret>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is PolarusLaserTurret minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;
                }

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        public bool HasPosition => PositionIndex > -1;

        public const float RotationTimerMax = 360;
        public ref float RotationTimer => ref NPC.ai[2];

        public static int BodyType()
        {
            return ModContent.NPCType<Polarus>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Polarus' Shield");
        }

        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lifeMax = 6000;
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

            SpawnTurrets();

            if (NPC.AnyNPCs(ModContent.NPCType<PolarusLaserTurret>()))
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
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

            float rad = (float)PositionIndex / Polarus.ShieldCount() * MathHelper.TwoPi;

            RotationTimer += 0.10f;
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
        }
    }
}
