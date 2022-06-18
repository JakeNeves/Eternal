using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Arkasama
{
    public class ArkasamaShield : ModNPC
    {
        private Player player;

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

        public bool HasPosition => PositionIndex > -1;

        public const float RotationTimerMax = 360;
        public ref float RotationTimer => ref NPC.ai[2];

        public static int BodyType()
        {
            return ModContent.NPCType<Arkasama>();
        }

        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lifeMax = 6400;
            NPC.defense = 40;
            NPC.width = 30;
            NPC.height = 88;
            NPC.damage = 65;
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

            Target();

            RotateNPCToTarget();
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

            float rad = (float)PositionIndex / Arkasama.ShieldCount() * MathHelper.TwoPi;

            RotationTimer += 0.5f;
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

            float speed = 36f;
            float inertia = 20;

            Vector2 moveTo = toDestinationNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
        }

        private void Target()
        {
            player = Main.player[NPC.target];
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }
    }
}
