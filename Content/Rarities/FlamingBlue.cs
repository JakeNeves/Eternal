using Eternal.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class FlamingBlue : ModRarity
    {
        public override Color RarityColor => EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 90 / 90f, [Color.Blue, Color.LightBlue, Color.Blue]);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Turquoise>();
            }

            return Type;
        }
    }
}
