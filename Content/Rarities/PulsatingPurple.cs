using Eternal.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class PulsatingPurple : ModRarity
    {
        public override Color RarityColor => EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.Violet, Color.Purple, Color.Violet]);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Aquamarine>();
            }

            return Type;
        }
    }
}
