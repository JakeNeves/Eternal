using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Maroon : ModRarity
    {
        public override Color RarityColor => new Color(199, 2, 2);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Magenta>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<Azure>();
            }

            return Type;
        }
    }
}
