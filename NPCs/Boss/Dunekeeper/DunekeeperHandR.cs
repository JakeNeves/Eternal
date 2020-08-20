using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    public class DunekeeperHandR : ModNPC
    {
        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dunekeeper's Hand");
        }

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 36;
            npc.lifeMax = 3000;
            npc.defense = 10;
            npc.damage = 12;
            npc.HitSound = SoundID.NPCDeath3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            DespawnHandler();
            Move(new Vector2(-100, 0));
            Target();
            RotateNPCToTarget();
        }

        private void Move(Vector2 offset)
        {
            speed = 9.25f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 0f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void Target()
        {
            player = Main.player[npc.target];
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }

    }
}
