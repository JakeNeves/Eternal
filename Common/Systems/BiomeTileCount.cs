using System;
using Eternal.Content.Tiles;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class BiomeTileCount : ModSystem
    {
        public int cometCount;
        public int grimstoneCount;
        public int shrineBrickCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            cometCount = tileCounts[ModContent.TileType<CometiteOre>()];
            grimstoneCount = tileCounts[ModContent.TileType<Grimstone>()];
            shrineBrickCount = tileCounts[ModContent.TileType<ShrineBrick>()];
        }
    }
}
