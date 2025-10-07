using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Ultramarine : ModRarity
    {
        public override Color RarityColor => new Color(12, 47, 242);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Azure>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<PulsatingPurple>();
            }

            return Type;
        }
    }
}
