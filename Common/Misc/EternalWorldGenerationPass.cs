using Eternal.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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

        #region The Beneath
        private static void GenBeneath()
        {
            while (true)
            {
                int benX = WorldGen.genRand.Next(400, Main.maxTilesX - 1200);
                int benY = WorldGen.genRand.Next((int)WorldGen.rockLayerHigh, Main.maxTilesY);
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 750), 80, ModContent.TileType<Grimstone>(), false, WorldGen.genRand.Next(9, 20), WorldGen.genRand.Next(-4, 4));
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 750), 80, ModContent.TileType<Grimstone>(), false, WorldGen.genRand.Next(-20, -9), WorldGen.genRand.Next(-4, 4));

                break;
            }
        }
        #endregion
    }
}
