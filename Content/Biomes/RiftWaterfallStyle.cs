using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class RiftWaterfallStyle : ModWaterfallStyle
    {
        public override void AddLight(int i, int j) =>
            Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), Color.DeepPink.ToVector3() * 0.75f);
    }
}
