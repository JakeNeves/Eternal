using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Azure : ModRarity
    {
        public override Color RarityColor => new Color(0, 200, 255);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Maroon>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<Ultramarine>();
            }

            return Type;
        }
    }
}
