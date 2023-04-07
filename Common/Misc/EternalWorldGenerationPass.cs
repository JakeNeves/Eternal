using Eternal.Content.Tiles;
using Eternal.Content.Walls;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Common.Misc
{
    public static class EternalWorldGenerationPass
    {
        #region Biome GenPass
        public static void GenBeneath(GenerationProgress progress, GameConfiguration config)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;
                if (attempts > 20)
                    break;

                progress.Message = "Darkening a spot in the world";
                GenBeneath();

                break;
            }
        }
        #endregion

        public static void GenerateStructures(GenerationProgress progress, GameConfiguration config)
        {
            // TODO: Inferno Sepulture (Most likely 1.1 update)

            progress.Message = "Placing Shrine";
            GenerateShrine();
        }

        #region Shrine
        private static void GenerateShrine()
        {
            int[,] tile =
            {
            {0,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,0 },
            {0,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,0,0,0,0 },
            {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0 },
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };

            int[,] wall =
            {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0 },
            };

            bool placed = false;
            while (!placed)
            {
                int structX = WorldGen.genRand.Next(Main.maxTilesX / 6, Main.maxTilesX / 6 * 5); 

                if (WorldGen.genRand.NextBool())
                {
                    structX = Main.maxTilesX - structX;
                }

                int structY = 0;
                while (!WorldGen.SolidTile(structX, structY) && structY <= Main.worldSurface)
                {
                    structY++;
                }

                if (structY > Main.worldSurface)
                {
                    continue;
                }

                Tile _tile = Main.tile[structX, structY];
                if (_tile.TileType != TileID.Grass && _tile.TileType != TileID.Ebonstone && _tile.TileType != TileID.Crimstone && _tile.TileType != TileID.Dirt)
                {
                    continue;
                }

                PlaceShrine(structX, structY - 1, tile, wall);
                placed = true;
            }
        }

        private static void PlaceShrine(int i, int j, int[,] BlocksArray, int[,] WallsArray)
        {
            for (int y = 0; y < BlocksArray.GetLength(0); y++)
            {
                for (int x = 0; x < BlocksArray.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (BlocksArray[y, x])
                        {
                            case 0:
                                break;
                            case 1:
                                WorldGen.KillWall(k, l);
                                Framing.GetTileSafely(k, l).ClearTile();
                                break;
                        }
                    }
                }
            }

            for (int y = 0; y < WallsArray.GetLength(0); y++)
            {
                for (int x = 0; x < WallsArray.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (WallsArray[y, x])
                        {
                            case 0:
                                break;
                            case 1:
                                WorldGen.KillWall(k, l);
                                Framing.GetTileSafely(k, l).ClearTile();
                                break;
                        }
                    }
                }
            }

            for (int y = 0; y < BlocksArray.GetLength(0); y++)
            {
                for (int x = 0; x < BlocksArray.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (BlocksArray[y, x])
                        {
                            case 0:
                                break;
                            case 1:
                                WorldGen.PlaceTile(k, l, ModContent.TileType<ShrineBrick>());
                                tile.HasTile = true;
                                break;
                        }
                    }
                }
            }

            for (int y = 0; y < WallsArray.GetLength(0); y++)
            {
                for (int x = 0; x < WallsArray.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (WallsArray[y, x])
                        {
                            case 0:
                                break;
                            case 1:
                                WorldGen.PlaceWall(k, l, ModContent.WallType<ShrineBrickWall>());
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        #region The Beneath
        private static void GenBeneath()
        {
            while (true)
            {
                int benX = WorldGen.genRand.Next(400, Main.maxTilesX - 1200);
                int benY = WorldGen.genRand.Next((int)GenVars.rockLayerHigh, Main.maxTilesY);
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 750), 80, ModContent.TileType<Grimstone>(), false, WorldGen.genRand.Next(9, 20), WorldGen.genRand.Next(-4, 4));
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 750), 80, ModContent.TileType<Grimstone>(), false, WorldGen.genRand.Next(-20, -9), WorldGen.genRand.Next(-4, 4));

                break;
            }
        }
        #endregion

        /// <summary>
        /// This method could be temperary
        /// </summary>
        public static void DropComet()
        {
            while (true)
            {
                int comX = WorldGen.genRand.Next(Main.rand.Next(-450, 450), Main.maxTilesX - 1200);
                int comY = WorldGen.genRand.Next((int)GenVars.worldSurface, Main.maxTilesY);
                WorldGen.TileRunner(comX, comY, WorldGen.genRand.Next(20, 45), 80, ModContent.TileType<CometiteOre>(), false, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(-2, 2));
                //WorldGen.TileRunner(comX, comY, WorldGen.genRand.Next(20, 45), 80, ModContent.TileType<CometiteOre>(), false, WorldGen.genRand.Next(-4, -8), WorldGen.genRand.Next(-2, 2));

                break;
            }
        }
    }
}
