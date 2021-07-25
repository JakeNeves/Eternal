using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Eternal.Projectiles.Enemy;
using System.Linq;

namespace Eternal.NPCs.DroxEvent
{
    public class DroxBomber : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drox Bomber");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 30;
            npc.damage = 8;
            npc.defense = 10;
            npc.lifeMax = 600;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().droxEvent)
            {
                int[] TileArray2 = { TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().droxEvent ? 2.09f : 0f;
            }
            return 0f;
        }

        public override void AI()
        {
            Target();
            Move(new Vector2(0, 0));
        }

        private void Move(Vector2 offset)
        {
            speed = 8.5f;
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
            npc.frameCounter += 0.5f;
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
            Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, ModContent.ProjectileType<DroxBomberBomb>(), 6, 0, Main.myPlayer, 0f, 0f);
        }

    }
}
