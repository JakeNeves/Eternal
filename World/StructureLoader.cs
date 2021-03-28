﻿using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework;


namespace Eternal.World
{
	public static class StructureLoader
	{
		private static string INTERNAL_STRUCTURE_PATH;

		private static Dictionary<string, Structure> _structures;
		private static Mod _mod;

		public static void Load(Mod mod)
        {
			_mod = mod;
			INTERNAL_STRUCTURE_PATH = "Structures/";

			_structures["Bastion"] = new Structure(mod, "Bastion");
        }

		//public static Structure GetStructure(string name) => _structures[name];

		public static void Unload()
		{
			_structures = null;
			_mod = null;
			INTERNAL_STRUCTURE_PATH = null;
		}

		public class Structure
		{
			//Copied and Pasted all of this from the Spirit Mod Repo, credit to PhoenixBladez and his team.
			public int width;
			public int height;
			public Tile[,] tiles;

			private bool _valid;
			public bool Valid => _valid;

			public Structure(Mod mod, string name, int invalidTileReplace = 0)
			{
				_valid = true;

				using (BinaryReader reader = new BinaryReader(mod.GetFileStream(INTERNAL_STRUCTURE_PATH + name + ".str")))
				{
					int version = reader.ReadInt32();

					int typeCount = reader.ReadInt32();
					TileWallType[] tileTypes = new TileWallType[typeCount];
					for (int i = 0; i < typeCount; i++)
					{
						tileTypes[i] = new TileWallType(reader);
					}

					typeCount = reader.ReadInt32();
					TileWallType[] wallTypes = new TileWallType[typeCount];
					for (int i = 0; i < typeCount; i++)
					{
						wallTypes[i] = new TileWallType(reader);
					}

					width = reader.ReadInt32();
					height = reader.ReadInt32();

					tiles = new Tile[width, height];

					for (int x = 0; x < width; x++)
					{
						for (int y = 0; y < height; y++)
						{
							if (reader.ReadBoolean())
							{
								Tile tile = new Tile();

								int type = reader.ReadInt32();
								if (type < 0 || type > tileTypes.Length)
								{
									_mod.Logger.Warn("Blueprint tile type incorrect? value: " + type);
									return;
								}
								tile.type = tileTypes[type].type;
								if (tileTypes[type].type == 9999)
								{
									_valid = false;
									tile.type = (ushort)invalidTileReplace;
								}

								tile.bTileHeader = reader.ReadByte();
								tile.bTileHeader2 = reader.ReadByte();
								tile.bTileHeader3 = reader.ReadByte();
								tile.sTileHeader = reader.ReadUInt16();
								tile.frameX = 0;
								tile.frameY = 0;
								if (reader.ReadBoolean())
								{
									tile.frameX = reader.ReadInt16();
									tile.frameY = reader.ReadInt16();
								}
								tile.liquid = reader.ReadByte();

								int wallType = reader.ReadInt32();
								if (wallType < 0 || wallType > wallTypes.Length)
								{
									_mod.Logger.Warn("Blueprint wall type incorrect? value: " + wallType);
									return;
								}
								tile.wall = wallTypes[wallType].type;
								if (wallTypes[wallType].type == 9999)
								{
									_valid = false;
									tile.wall = 0;
								}

								tiles[x, y] = tile;
							}
						}
					}
				}
			}

			/// <summary>
			/// Places the structure, removing tiles if necessary.
			/// </summary>
			public void PlaceForce(int x, int y, out Point[] chestLocations)
			{
				if (!_valid)
				{
					chestLocations = null;
					return;
				}

				List<Point> containers = new List<Point>();
				for (int x1 = 0; x1 < width; x1++)
				{
					for (int y1 = 0; y1 < height; y1++)
					{
						if (tiles[x1, y1] != null)
						{
							Main.tile[x + x1, y + y1] = new Tile(tiles[x1, y1]);
							DoContainerCheck(ref containers, x, y, x1, y1);
						}
					}
				}
				chestLocations = containers.ToArray();
			}

			public void Place(int x, int y, out Point[] chestLocations, bool removeNonSolids = false)
			{
				if (!_valid)
				{
					chestLocations = null;
					return;
				}

				List<Point> containers = new List<Point>();
				for (int x1 = 0; x1 < width; x1++)
				{
					for (int y1 = 0; y1 < height; y1++)
					{
						if (tiles[x1, y1] != null && (!Main.tile[x + x1, y + y1].active() || (removeNonSolids && !Main.tileSolid[Main.tile[x + x1, y + y1].type])))
						{
							Main.tile[x + x1, y + y1] = new Tile(tiles[x1, y1]);
							DoContainerCheck(ref containers, x, y, x1, y1);
						}
					}
				}
				chestLocations = containers.ToArray();
			}

			private void DoContainerCheck(ref List<Point> containers, int x, int y, int x1, int y1)
			{
				if (Main.tileContainer[tiles[x1, y1].type])
				{
					ushort type = tiles[x1, y1].type;
					if (Framing.GetTileSafely(x + x1 - 1, y + y1).type != type &&
						Framing.GetTileSafely(x + x1, y + y1 - 1).type != type)
					{
						containers.Add(new Point(x + x1, y + y1));
					}
				}
			}

			public struct TileWallType
			{
				public bool tile;
				public bool modded;
				public ushort type;
				public string modAndName;

				public TileWallType(bool tile, bool m, ushort t, string s)
				{
					this.tile = tile;
					modded = m;
					type = t;
					modAndName = s;
				}

				public TileWallType(BinaryReader reader)
				{
					modded = false;
					type = 0;
					modAndName = "";
					tile = false;

					Read(reader);
				}

				public void Write(BinaryWriter writer)
				{
					writer.Write(tile);
					writer.Write(modded);
					if (modded)
					{
						writer.Write(modAndName);
					}
					else
					{
						writer.Write(type);
					}
				}

				public void Read(BinaryReader reader)
				{
					tile = reader.ReadBoolean();
					modded = reader.ReadBoolean();
					if (modded)
					{
						type = 9999;
						modAndName = reader.ReadString();
						string[] args = modAndName.Split('#');
						Mod mod = ModLoader.GetMod(args[0]);
						if (mod != null)
						{
							if (tile)
							{
								ModTile mtile = mod.GetTile(args[1]);
								if (mtile != null)
								{
									type = mtile.Type;
								}
							}
							else
							{
								ModWall mwall = mod.GetWall(args[1]);
								if (mwall != null)
								{
									type = mwall.Type;
								}
							}
						}
					}
					else
					{
						type = reader.ReadUInt16();
					}
				}
			}
		}
	}
}
