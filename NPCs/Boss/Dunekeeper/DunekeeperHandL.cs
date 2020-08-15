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
        int Speed = 10;
        int Acceleration = 5;

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
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            Move();
            Target();
            RotateNPCToTarget();
        }

        private void Move()
        {
            Vector2 StartPosition = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float DirectionX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - StartPosition.X;
            float DirectionY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120 - StartPosition.Y;
            float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
            float Num = Speed / Length;
            DirectionX = DirectionX * Num;
            DirectionY = DirectionY * Num;
            if (npc.velocity.X < DirectionX)
            {
                npc.velocity.X = npc.velocity.X + Acceleration;
                if (npc.velocity.X < 0 && DirectionX > 0)
                    npc.velocity.X = npc.velocity.X + Acceleration;
            }
            else if (npc.velocity.X > DirectionX)
            {
                npc.velocity.X = npc.velocity.X - Acceleration;
                if (npc.velocity.X > 0 && DirectionX < 0)
                    npc.velocity.X = npc.velocity.X - Acceleration;
            }
            if (npc.velocity.Y < DirectionY)
            {
                npc.velocity.Y = npc.velocity.Y + Acceleration;
                if (npc.velocity.Y < 0 && DirectionY > 0)
                    npc.velocity.Y = npc.velocity.Y + Acceleration;
            }
            else if (npc.velocity.Y > DirectionY)
            {
                npc.velocity.Y = npc.velocity.Y - Acceleration;
                if (npc.velocity.Y > 0 && DirectionY < 0)
                    npc.velocity.Y = npc.velocity.Y - Acceleration;
            }
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

    }
}
