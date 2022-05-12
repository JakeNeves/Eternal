using System;
using Eternal.Content.Tiles;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class BiomeTileCount : ModSystem
    {
        public int cometCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            cometCount = tileCounts[ModContent.TileType<CometiteOre>()];
        }
    }
}
