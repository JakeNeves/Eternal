using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    public class DunekeeperHandL : ModNPC
    {
        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dunekeeper");
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
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            player = Main.player[npc.target];

            Move(new Vector2(-90f, 0f));

            DespawnHandler();

            npc.spriteDirection = npc.direction;

        }

        private void Move(Vector2 offset)
        {
         
            speed = 10f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
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

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    }
}
