using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Microsoft.Xna.Framework;
using System;

namespace Eternal.NPCs.DroxEvent
{
    public class BubbleMachine : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drox Bubble Machine");
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 44;
            npc.damage = 18;
            npc.defense = 10;
            npc.lifeMax = 600;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
        }

        public override void AI()
        {
            Target();
            Move(new Vector2(0, 0));
        }

        private void Move(Vector2 offset)
        {
            speed = 5f;
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

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        private void Target()
        {
            npc.TargetClosest(true);
            player = Main.player[npc.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DroxCore>());
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DroxPlate>(), Main.rand.Next(2, 8));
        }

    }
}
