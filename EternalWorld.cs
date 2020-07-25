using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Tiles;
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Eternal
{
    class EternalWorld : ModWorld
    {
        public static bool hellMode = false;

		public static bool downedCarmaniteScouter = false;

		public override void Initialize()
		{
			downedCarmaniteScouter = false;
		}

		public override TagCompound Save()
		{
			var downed = new List<string>();
			if (downedCarmaniteScouter) downed.Add("eternal");

			return new TagCompound
			{
				{"downed", downed }
			};

		}

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedCarmaniteScouter = downed.Contains("eternal");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedCarmaniteScouter = flags[0];
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedCarmaniteScouter;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedCarmaniteScouter = flags[0];
        }


        private void EternalOres(GenerationProgress progress)
        {
            progress.Message = "Generating Eternalism";

			for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
			{
				int x = WorldGen.genRand.Next(0, Main.maxTilesX);
				int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
				WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), TileType<Rudanium>());
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 9), TileType<TritalodiumOre>());

            }
		}

        public static int thunderduneBiome = 0;
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (genIndex == -1)
            {
                return;
            }
            tasks.Insert(genIndex + 1, new PassLegacy("Thunderdune", delegate (GenerationProgress progress)
            {
                progress.Message = "Thunderfication";
                for (int i = 0; i < Main.maxTilesX / 500; i++)
                {
                    int X = WorldGen.genRand.Next(1, Main.maxTilesX - 250);
                    int Y = WorldGen.genRand.Next((int)WorldGen.worldSurface - 200, Main.maxTilesY - 200);
                    int TileType = TileType<Dunesand>() + TileType<Dunestone>();


                    WorldGen.TileRunner(X, Y, 325, WorldGen.genRand.Next(100, 300), TileType, false, 0f, 0f, true, true);
                }

                progress.Message = "Carving the Temple";
                MakeDuneTemple();

            }));
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            thunderduneBiome = tileCounts[TileType<Dunesand>()] + tileCounts[TileType<Dunestone>()];
        }

        //Dune Temple
        private void MakeDuneTemple()
        {
            float widthScale = Main.maxTilesX / 42000f;
            int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f / widthScale));
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
                        while (!Main.tile[i, j].active() && (double)j < EternalWorld.thunderduneBiome)
                        {
                            j++;
                        }
                        if (Main.tile[i, j].type == TileType<Dunesand>() || Main.tile[i, j].type == TileType<Dunestone>())
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
                                    success = PlaceDuneTemple(i, j);
                                }
                            }
                        }
                    }
                }
            }
        }

        private readonly int[,] _duneTempleShape =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,2,2,2,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1},
            {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public bool PlaceDuneTemple(int i, int j)
        {
            if (!WorldGen.SolidTile(i, j + 1))
            {
                return false;
            }

            if(Main.tile[i, j].active())
            {
                return false;
            }
            if (j < 150)
            {
                return false;
            }

            for (int y = 0; y < _duneTempleShape.GetLength(0); y++)
            {
                for (int x = 0; x < _duneTempleShape.GetLength(1); x++)
                {
                    int k = i - 3 + x;
                    int l = j - 6 + y;
                    if(WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, 1);
                        switch (_duneTempleShape[y, x])
                        {
                            case 1:
                                tile.type = (ushort)TileType<Dunestone>();
                                tile.active(true);
                            break;

                            case 2:
                                tile.type = TileID.TeamBlockYellowPlatform;
                                tile.active(true);
                            break;
                        }

                    }
                }
            }

            return true;
        }

    }
}
