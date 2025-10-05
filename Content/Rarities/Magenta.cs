using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Magenta : ModRarity
    {
        public override Color RarityColor => new Color(220, 84, 247);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Teal>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<Maroon>();
            }

            return Type;
        }
    }
}
