using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Eternal.Tiles;
using System;
using System.IO;
using System.Collections.Generic;
using Eternal.NPCs;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;

namespace Eternal
{
    public class EternalWorld : ModWorld
    {
        #region WorldGenTesting
        //public static bool JustPressed(Keys key)
        //{
        //    return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
        //}

        //private void TestMethod(int x, int y)
        //{
        //    Dust.QuickBox(new Vector2(x, y) * 16, new Vector2(x + 1, y + 1) * 16, 2, Color.YellowGreen, null);
        //
        //    WorldGen.TileRunner(x - 1, y, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(2, 8), TileID.Stone);
        //}
        #endregion

        #region BiomeTiles
        public static int DuneTiles;
        public static int LabrynthTiles;
        public static int BeneathTiles;
        public static int AshpitTiles;
        #endregion

        /// <summary>
        ///     Just Like a Master Mode+ or an Expert Mode++
        ///     <param>
        ///         hellMode
        ///     </param>
        /// </summary>
        public static bool hellMode = false;

        #region DownedBosses
        //Pre-Hardmode
        public static bool downedCarmaniteScouter = false;
        public static bool downedDunekeeper = false;
        //Hardmode
        public static bool downedIncinerius = false;
        public static bool downedSubzeroElemental = false;
        //Post-Moon Lord
        public static bool downedSubzeroElementalP2 = false;
        public static bool downedCosmicApparition = false;
        #endregion

        public override void TileCountsAvailable(int[] tileCounts)
        {
            LabrynthTiles = tileCounts[TileType<LabrynthStone>()];
            DuneTiles = tileCounts[TileType<Dunestone>()] + tileCounts[TileType<Dunestone>()];
            thunderduneBiome = DuneTiles;
            commet = tileCounts[TileType<CometiteOre>()];
            labrynth = LabrynthTiles;
            BeneathTiles = tileCounts[TileType<Grimstone>()];
            theBeneath = BeneathTiles;
            ashpit = tileCounts[TileType<HeatslateGrowth>()];
        }

        public override void Initialize()
		{
            //Pre-Hardmode
			downedCarmaniteScouter = false;
            downedDunekeeper = false;
            //Hardmode
            downedIncinerius = false;
            downedSubzeroElemental = false;
            //Post-Moon Lord
            downedSubzeroElementalP2 = false;
            downedCosmicApparition = false;

            hellMode = false;
        }

		public override TagCompound Save()
		{
			var downed = new List<string>();
            var difficulty = new List<string>();
            #region DownedBosses
            //Pre-Hardmode
            if (downedCarmaniteScouter) downed.Add("eternal");
            if (downedDunekeeper) downed.Add("eternal");
            //Hardmode
            if (downedIncinerius) downed.Add("eternal");
            if (downedSubzeroElemental) downed.Add("eternal");
            //Post-Moon Lord
            if (downedSubzeroElementalP2) downed.Add("eternal");
            if (downedCosmicApparition) downed.Add("eternal");
            #endregion

            if (hellMode) difficulty.Add("eternal");

            return new TagCompound
			{
				{"downed", downed },
                {"difficulty", difficulty }
            };

		}

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            var difficulty = tag.GetList<string>("difficulty");
            #region DownedBosses
            //Pre-Hardmode
            downedCarmaniteScouter = downed.Contains("eternal");
            downedDunekeeper = downed.Contains("eternal");
            //Hardmode
            downedIncinerius = downed.Contains("eternal");
            downedSubzeroElemental = downed.Contains("eternal");
            //Post-Moon Lord
            downedSubzeroElementalP2 = downed.Contains("eternal");
            downedCosmicApparition = downed.Contains("eternal");
            #endregion

            hellMode = difficulty.Contains("eternal");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                #region DownedBossFlags
                //Pre-Hardmode
                downedCarmaniteScouter = flags[0];
                downedDunekeeper = flags[0];
                //Hardmode
                downedIncinerius = flags[0];
                downedSubzeroElemental = flags[0];
                //Post-Moon Lord
                downedSubzeroElementalP2 = flags[0];
                downedCosmicApparition = flags[0];
                #endregion
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            BitsByte difficultyFlag = new BitsByte();
            #region DownedBossFlags
            //Pre-Hardmode
            flags[0] = downedCarmaniteScouter;
            flags[1] = downedDunekeeper;
            //Hardmode
            flags[2] = downedIncinerius;
            flags[3] = downedSubzeroElemental;
            //Post-Moon Lord
            flags[4] = downedSubzeroElementalP2;
            flags[5] = downedCosmicApparition;
            #endregion

            difficultyFlag[0] = hellMode;

            writer.Write(difficultyFlag);
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            BitsByte difficultyFlag = new BitsByte();
            #region DownedBossFlags
            //Pre-Hardmode
            downedCarmaniteScouter = flags[0];
            downedDunekeeper = flags[0];
            //Hardmode
            downedIncinerius = flags[0];
            downedSubzeroElemental = flags[0];
            //Post-Moon Lord
            downedSubzeroElementalP2 = flags[0];
            downedCosmicApparition = flags[0];
            #endregion

            difficultyFlag[0] = hellMode;
        }

        public override void PostUpdate()
        {
            Player player = Main.LocalPlayer;
            EternalPlayer modPlayer = player.GetEternalPlayer();

            EternalGlobalNPC.hellModeDifficulty = hellMode;

            //if (JustPressed(Keys.D1))
            //    TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);

        }

        #region commet
        //ripped this from tremor, credit to whoever wrote this originally...
        public static void DropComet()
        {
            bool flag = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active)
                {
                    flag = false;
                    break;
                }
            }
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(400f * num2);
            for (int j = 5; j < Main.maxTilesX - 5; j++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[j, num4].active() && Main.tile[j, num4].type == (ushort)TileType<CometiteOre>())
                    {
                        num++;
                        if (num > num3)
                        {
                            return;
                        }
                    }
                    num4++;
                }
            }
            float num5 = 600f;
            while (!flag)
            {
                float num6 = Main.maxTilesX * 0.08f;
                int num7 = Main.rand.Next(150, Main.maxTilesX - 150);
                while (num7 > Main.spawnTileX - num6 && num7 < Main.spawnTileX + num6)
                {
                    num7 = Main.rand.Next(150, Main.maxTilesX - 150);
                }
                int k = (int)(Main.worldSurface * 0.3);
                while (k < Main.maxTilesY)
                {
                    if (Main.tile[num7, k].active() && Main.tileSolid[Main.tile[num7, k].type])
                    {
                        int num8 = 0;
                        int num9 = 15;
                        for (int l = num7 - num9; l < num7 + num9; l++)
                        {
                            for (int m = k - num9; m < k + num9; m++)
                            {
                                if (WorldGen.SolidTile(l, m))
                                {
                                    num8++;
                                    if (Main.tile[l, m].type == 189 || Main.tile[l, m].type == 202)
                                    {
                                        num8 -= 100;
                                    }
                                }
                                else if (Main.tile[l, m].liquid > 0)
                                {
                                    num8--;
                                }
                            }
                        }
                        if (num8 < num5)
                        {
                            num5 -= 0.5f;
                            break;
                        }
                        flag = Comet(num7, k);
                        if (flag)
                        {
                        }
                        break;
                    }
                    k++;
                }
                if (num5 < 100f)
                {
                    return;
                }
            }
        }

        //also ripped this from tremor, credit to whoever wrote this originally...
        public static bool Comet(int i, int j)
        {
            if (i < 50 || i > Main.maxTilesX - 50)
            {
                return false;
            }
            if (j < 50 || j > Main.maxTilesY - 50)
            {
                return false;
            }
            int num = 35;
            Rectangle rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
            for (int k = 0; k < 255; k++)
            {
                if (Main.player[k].active)
                {
                    Rectangle value = new Rectangle((int)(Main.player[k].position.X + Main.player[k].width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(Main.player[k].position.Y + Main.player[k].height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                    if (rectangle.Intersects(value))
                    {
                        return false;
                    }
                }
            }
            for (int l = 0; l < 200; l++)
            {
                if (Main.npc[l].active)
                {
                    Rectangle value2 = new Rectangle((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
                    if (rectangle.Intersects(value2))
                    {
                        return false;
                    }
                }
            }
            for (int m = i - num; m < i + num; m++)
            {
                for (int n = j - num; n < j + num; n++)
                {
                    if (Main.tile[m, n].active() && Main.tile[m, n].type == 21)
                    {
                        return false;
                    }
                }
            }
            num = WorldGen.genRand.Next(17, 23);
            for (int num2 = i - num; num2 < i + num; num2++)
            {
                for (int num3 = j - num; num3 < j + num; num3++)
                {
                    if (num3 > j + Main.rand.Next(-2, 3) - 5)
                    {
                        float num4 = Math.Abs(i - num2);
                        float num5 = Math.Abs(j - num3);
                        float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                        if (num6 < num * 0.9 + Main.rand.Next(-4, 5))
                        {
                            if (!Main.tileSolid[Main.tile[num2, num3].type])
                            {
                                Main.tile[num2, num3].active(false);
                            }
                            Main.tile[num2, num3].type = (ushort)TileType<CometiteOre>();
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(8, 14);
            for (int num7 = i - num; num7 < i + num; num7++)
            {
                for (int num8 = j - num; num8 < j + num; num8++)
                {
                    if (num8 > j + Main.rand.Next(-2, 3) - 4)
                    {
                        float num9 = Math.Abs(i - num7);
                        float num10 = Math.Abs(j - num8);
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        if (num11 < num * 0.8 + Main.rand.Next(-3, 4))
                        {
                            Main.tile[num7, num8].active(false);
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(25, 35);
            for (int num12 = i - num; num12 < i + num; num12++)
            {
                for (int num13 = j - num; num13 < j + num; num13++)
                {
                    float num14 = Math.Abs(i - num12);
                    float num15 = Math.Abs(j - num13);
                    float num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    if (num16 < num * 0.7)
                    {
                        if (Main.tile[num12, num13].type == 5 || Main.tile[num12, num13].type == 32 || Main.tile[num12, num13].type == 352)
                        {
                            WorldGen.KillTile(num12, num13, false, false, false);
                        }
                        Main.tile[num12, num13].liquid = 0;
                    }
                    if (Main.tile[num12, num13].type == (ushort)TileType<CometiteOre>())
                    {
                        if (!WorldGen.SolidTile(num12 - 1, num13) && !WorldGen.SolidTile(num12 + 1, num13) && !WorldGen.SolidTile(num12, num13 - 1) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            Main.tile[num12, num13].active(false);
                        }
                        else if ((Main.tile[num12, num13].halfBrick() || Main.tile[num12 - 1, num13].topSlope()) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            Main.tile[num12, num13].active(false);
                        }
                    }
                    WorldGen.SquareTileFrame(num12, num13, true);
                    WorldGen.SquareWallFrame(num12, num13, true);
                }
            }
            num = WorldGen.genRand.Next(23, 32);
            for (int num17 = i - num; num17 < i + num; num17++)
            {
                for (int num18 = j - num; num18 < j + num; num18++)
                {
                    if (num18 > j + WorldGen.genRand.Next(-3, 4) - 3 && Main.tile[num17, num18].active() && Main.rand.Next(10) == 0)
                    {
                        float num19 = Math.Abs(i - num17);
                        float num20 = Math.Abs(j - num18);
                        float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
                        if (num21 < num * 0.8)
                        {
                            if (Main.tile[num17, num18].type == 5 || Main.tile[num17, num18].type == 32 || Main.tile[num17, num18].type == 352)
                            {
                                WorldGen.KillTile(num17, num18, false, false, false);
                            }
                            Main.tile[num17, num18].type = (ushort)TileType<CometiteOre>();
                            WorldGen.SquareTileFrame(num17, num18, true);
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(30, 38);
            for (int num22 = i - num; num22 < i + num; num22++)
            {
                for (int num23 = j - num; num23 < j + num; num23++)
                {
                    if (num23 > j + WorldGen.genRand.Next(-2, 3) && Main.tile[num22, num23].active() && Main.rand.Next(20) == 0)
                    {
                        float num24 = Math.Abs(i - num22);
                        float num25 = Math.Abs(j - num23);
                        float num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
                        if (num26 < num * 0.85)
                        {
                            if (Main.tile[num22, num23].type == 5 || Main.tile[num22, num23].type == 32 || Main.tile[num22, num23].type == 352)
                            {
                                WorldGen.KillTile(num22, num23, false, false, false);
                            }
                            Main.tile[num22, num23].type = (ushort)TileType<CometiteOre>();
                            WorldGen.SquareTileFrame(num22, num23, true);
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        public void GenBeneath()
        {
            WorldGen.TileRunner(32, 32, WorldGen.genRand.Next(90, 200), 40, TileType<Grimstone>(), true, WorldGen.genRand.Next(9, 20), WorldGen.genRand.Next(-2, 2));
            WorldGen.TileRunner(32, 32, WorldGen.genRand.Next(90, 200), 40, TileType<Grimstone>(), true, WorldGen.genRand.Next(-20, -9), WorldGen.genRand.Next(-2, 2));
        }

        public void createShrine()
        {
            float widthScale = Main.maxTilesX / 4200f;
            int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f * widthScale));
            for (int k = 0; k < numberToGenerate; k++)
            {
                bool success = false;
                int attempts = 0;
                while (!success)
                {
                    attempts++;
                    if (attempts > 1000)
                    {
                        success = true;
                        continue;
                    }
                    int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                    if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 + 50)
                    {
                        int j = 0;
                        while (!Main.tile[i, j].active() && (double)j < Main.worldSurface)
                        {
                            j++;
                        }
                        if (Main.tile[i, j].type == TileID.Dirt)
                        {
                            j--;
                            if (j > 150)
                            {
                                bool placementOK = true;
                                for (int l = i - 4; l < i + 4; l++)
                                {
                                    for (int m = j - 6; m < j + 20; m++)
                                    {
                                        if (Main.tile[l, m].active())
                                        {
                                            int type = (int)Main.tile[l, m].type;
                                            if (type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Cloud || type == TileID.RainCloud)
                                            {
                                                placementOK = false;
                                            }
                                        }
                                    }
                                }
                                if (placementOK)
                                {
                                    success = PlaceShrine(i, j);
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            /*int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (shiniesIndex == -1)
            {
                tasks.Insert(shiniesIndex + 1, new PassLegacy("Generating Eternal Ores", EternalOres));

                return;
            }*/

            int GraniteIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
            if (GraniteIndex != -1)
            {
                tasks.Insert(GraniteIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                {
                    progress.Message = "Making Grim Caves...";
                    GenBeneath();
                }));
            }

            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (LivingTreesIndex != -1)
            {
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                {
                    progress.Message = "Burrying Shrine";
                    createShrine();
                }));
            }
        }

        #region Ark Shrine
        private readonly int[,] _shrineShape =
        {
            {0,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,0,0,0,0 },
            {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        private readonly int[,] _shrineShapeWall =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        };
        

        public bool PlaceShrine(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1))
            {
                return false;
            }
            if (Main.tile[i, j].active())
            {
                return false;
            }
            if (j < 150)
            {
                return false;
            }

            for (int y = 0; y < _shrineShape.GetLength(0); y++)
            {
                for (int x = 0; x < _shrineShape.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_shrineShape[y, x])
                        {
                            case 1:
                                tile.type = (ushort)TileType<LabrynthStone>();
                                break;
                        }
                        switch (_shrineShapeWall[y, x])
                        {
                            case 1:
                                tile.wall = (ushort)WallType<Walls.LabrynthStoneWall>();
                                break;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region ShadowTomb
        private readonly int[,] _tombShape =
        {
            {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0 },
            {1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,2,0,0,2,0,2,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,2,2,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,2,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1 },
            {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1 },
            {1,1,1,1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,1,1,1 },
            {1,1,1,1,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,1,1,1 },
            {1,1,1,1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,2,2,2,1,1,1,1 },
            {1,1,1,1,2,2,2,2,0,0,0,0,3,0,0,0,0,2,2,2,2,1,1,1,1 },
            {1,1,1,1,1,2,2,2,2,2,1,1,1,1,1,2,2,2,2,2,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        private readonly int[,] _tombShapeWall =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        public bool PlaceTomb(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1))
            {
                return false;
            }
            if (Main.tile[i, j].active())
            {
                return false;
            }
            if (j < 150)
            {
                return false;
            }

            for (int y = 0; y < _shrineShape.GetLength(0); y++)
            {
                for (int x = 0; x < _shrineShape.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_shrineShape[y, x])
                        {
                            case 1:
                                tile.type = (ushort)TileType<GrimstoneBrick>();
                                break;
                            case 2:
                                tile.type = (ushort)TileType<Grimstone>();
                                break;
                            case 3:
                                tile.type = (ushort)TileType<Tiles.Interactive.ThunderRuneOfferingAltar>();
                                break;
                        }
                        switch (_shrineShapeWall[y, x])
                        {
                            case 1:
                                tile.wall = (ushort)WallType<Walls.GrimstoneWall>();
                                break;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        public void EternalOres(GenerationProgress progress)
        {
            progress.Message = "Generating Eternal Ores";

			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), TileType<Rudanium>());

            }
		}

        public static int thunderduneBiome = 0;
        public static int commet = 0;
        public static int labrynth = 0;
        public static int theBeneath = 0;
        public static int ashpit = 0;
    }
}
