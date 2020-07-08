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

namespace Eternal
{
    class EternalWorld : ModWorld
    {
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

			}
		}

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
                for (int i = 0; i < Main.maxTilesX / 900; i++)
                {
                    int X = WorldGen.genRand.Next(1, Main.maxTilesX - 300);
                    int Y = WorldGen.genRand.Next((int)WorldGen.worldSurface - 200, Main.maxTilesY - 200);
                    int TileType = mod.TileType("Dunesand");

                    WorldGen.TileRunner(X, Y, 350, WorldGen.genRand.Next(100, 300), TileType, false, 0f, 0f, true, true);
                }
            }));
        }
	
	public override void TileCountsAvailable(int[] tileCounts)
	{
		thunderduneBiome = tileCounts[TileType<Dunesand>()];
	}
	
    }
}
