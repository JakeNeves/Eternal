using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Turquoise : ModRarity
    {
        public override Color RarityColor => new Color(105, 235, 240);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Magenta>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<FlamingBlue>();
            }

            return Type;
        }
    }
}
