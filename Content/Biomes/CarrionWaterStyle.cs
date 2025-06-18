using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class CarrionWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<CarrionWaterfallStyle>().Slot;
        }

        public override int GetSplashDust()
        {
            return DustID.Blood;
        }

        public override int GetDropletGore()
        {
            return ModContent.GoreType<CarrionWaterDroplet>();
        }

        public override Color BiomeHairColor()
        {
            return Color.IndianRed;
        }

        public override byte GetRainVariant()
        {
            return (byte)Main.rand.Next(3);
        }

        public override Asset<Texture2D> GetRainTexture()
        {
            return ModContent.Request<Texture2D>("Eternal/Content/Biomes/CarrionRain");
        }
    }
}
