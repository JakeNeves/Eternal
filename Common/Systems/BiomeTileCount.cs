using System;
using Eternal.Content.Tiles;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class BiomeTileCount : ModSystem
    {
        public int biomeTileX;
        public int biomeTileY;

        public int cometCount;
        public int gloomrockCount;
        public int shinestoneCount;
        public int shrineBrickCount;
        public int redBasaltCount;
        public int hexedBasaltCount;
        public int carrionBlockCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            cometCount = tileCounts[ModContent.TileType<CometiteOre>()];
            gloomrockCount = tileCounts[ModContent.TileType<Gloomrock>()];
            shinestoneCount = tileCounts[ModContent.TileType<Shinestone>()];
            shrineBrickCount = tileCounts[ModContent.TileType<ShrineBrick>()];
            redBasaltCount = tileCounts[ModContent.TileType<RedBasaltBlock>()];
            hexedBasaltCount = tileCounts[ModContent.TileType<HexedBasaltBlock>()];
            carrionBlockCount = tileCounts[ModContent.TileType<RottenstoneBlock>()];
        }
    }
}
