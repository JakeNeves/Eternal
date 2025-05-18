using Eternal.Content.Tiles;
using StructureHelper.API;
using Terraria;
using Terraria.DataStructures;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Common.Misc
{
    public partial class EternalWorldGenerationPass : ModSystem
    {
        #region  GenPass
        public static void GenBeneath(GenerationProgress progress, GameConfiguration config)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;
                if (attempts > 20)
                    break;

                if (progress != null)
                    progress.Message = "Darkening a spot in the world";

                GenBeneath();

                break;
            }
        }

        public static void GenShrine(GenerationProgress progress, GameConfiguration config)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;
                if (attempts > 20)
                    break;

                if (progress != null)
                    progress.Message = "Placing a sword-reserved Shrine";

                GenShrine();

                break;
            }
        }

        public static void GenGehenna(GenerationProgress progress, GameConfiguration config)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;
                if (attempts > 20)
                    break;

                if (progress != null)
                    progress.Message = "Forging the Gehenna";

                GenGehenna();

                break;
            }
        }

        public static void GenMausoleum(GenerationProgress progress, GameConfiguration config)
        {
            int attempts = 0;

            while (true)
            {
                attempts++;
                if (attempts > 20)
                    break;

                if (progress != null)
                    progress.Message = "Placing a cursed Mausoleum";

                GenMausoleum();

                break;
            }
        }
        #endregion

        #region Structures
        private static void GenShrine() {
            int _shrineTileX = WorldGen.genRand.Next(WorldGen.genRand.Next(-400, 400), Main.maxTilesX - 1200);

            Generator.GetStructureDimensions("Content/Structures/Shrine", Eternal.Instance);
            Generator.GenerateStructure("Content/Structures/Shrine", new Point16(_shrineTileX, (int)Main.worldSurface - 120), Eternal.Instance);
        }

        private static void GenGehenna()
        {
            int _gehannaTileX = WorldGen.genRand.Next(WorldGen.genRand.Next(-400, 400), Main.maxTilesX - 1200);

            Generator.GetStructureDimensions("Content/Structures/Gehenna", Eternal.Instance);
            Generator.GenerateStructure("Content/Structures/Gehenna", new Point16(_gehannaTileX, (int)Main.UnderworldLayer), Eternal.Instance);
        }

        private static void GenMausoleum()
        {
            int _mausoTileX = WorldGen.genRand.Next(WorldGen.genRand.Next(-400, 400), Main.maxTilesX - 1200);

            Generator.GetStructureDimensions("Content/Structures/Mausoleum", Eternal.Instance);
            Generator.GenerateStructure("Content/Structures/Mausoleum", new Point16(_mausoTileX, (int)Main.rockLayer + 115), Eternal.Instance);
        }
        #endregion

        #region Mini Biomes
        private static void GenBeneath()
        {
            while (true)
            {
                int benX = WorldGen.genRand.Next(400, Main.maxTilesX - 1200);
                int benY = WorldGen.genRand.Next((int)GenVars.rockLayerHigh - 120, Main.maxTilesY);
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 600), 80, ModContent.TileType<Gloomrock>(), false, WorldGen.genRand.Next(9, 20), WorldGen.genRand.Next(-4, 4));
                WorldGen.TileRunner(benX, benY, WorldGen.genRand.Next(400, 600), 80, ModContent.TileType<Gloomrock>(), false, WorldGen.genRand.Next(-20, -9), WorldGen.genRand.Next(-4, 4));

                break;
            }
        }
        #endregion
    }
}
