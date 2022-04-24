using Eternal.Items.Materials;
using Eternal.Items.Weapons.Magic;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Comet
{
    public class Sightseer : ModNPC
    {

        private Player player;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 32;
            npc.damage = 100;
            npc.defense = 15;
            if (EternalWorld.downedCosmicApparition)
                npc.lifeMax = 2000;
            else
                npc.lifeMax = 1100;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.value = 50f;
            npc.noTileCollide = true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SightseerHead"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SightseerBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SightseerWing"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SightseerWing"), 1f);
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
            }
        }

        private static int[] SpawnTiles = { };

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }

            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);

            float speed = 10f;
            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.05F;
                    }
                }
            }
            float num10 = speed / length;
            xDir = xDir * num10;
            yDir = yDir * num10;
            if (npc.velocity.X < xDir)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0 && xDir > 0)
                    npc.velocity.X = npc.velocity.X + acceleration;
            }
            else if (npc.velocity.X > xDir)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0 && xDir < 0)
                    npc.velocity.X = npc.velocity.X - acceleration;
            }
            if (npc.velocity.Y < yDir)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0 && yDir > 0)
                    npc.velocity.Y = npc.velocity.Y + acceleration;
            }
            else if (npc.velocity.Y > yDir)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0 && yDir < 0)
                    npc.velocity.Y = npc.velocity.Y - acceleration;
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RainmakerStave>());
            }
            if (EternalWorld.downedCosmicApparition)
            {
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InterstellarSingularity>(), Main.rand.Next(5, 20));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<StarmetalBar>(), Main.rand.Next(10, 75));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Astragel>(), Main.rand.Next(10, 50));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GalaxianPlating>(), Main.rand.Next(3, 12));
            }
        }

    }
}
