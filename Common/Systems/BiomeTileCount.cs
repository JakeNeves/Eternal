using System;
using Eternal.Content.Tiles;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class BiomeTileCount : ModSystem
    {
        public int cometCount;
        public int gloomrockCount;
        public int shinestoneCount;
        public int shrineBrickCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            cometCount = tileCounts[ModContent.TileType<CometiteOre>()];
            gloomrockCount = tileCounts[ModContent.TileType<Gloomrock>()];
            shinestoneCount = tileCounts[ModContent.TileType<Shinestone>()];
            shrineBrickCount = tileCounts[ModContent.TileType<ShrineBrick>()];
        }
    }
}
