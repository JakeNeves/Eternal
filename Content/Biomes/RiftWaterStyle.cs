using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class RiftWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<RiftWaterfallStyle>().Slot;
        }

        public override int GetSplashDust()
        {
            return ModContent.DustType<CosmicSpirit>();
        }

        public override int GetDropletGore()
        {
            return ModContent.Find<ModGore>("Eternal/RiftAnomalyShard").Type;
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 0.75f;
            g = 0f;
            b = 0.75f;
        }

        public override Color BiomeHairColor()
        {
            return Color.DeepPink;
        }

        public override byte GetRainVariant()
        {
            return (byte)Main.rand.Next(3);
        }

        public override Asset<Texture2D> GetRainTexture()
        {
            return ModContent.Request<Texture2D>("Eternal/Content/Biomes/RiftRain");
        }
    }
}
