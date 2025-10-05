using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Aquamarine : ModRarity
    {
        public override Color RarityColor => new Color(18, 151, 166);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<Ultramarine>();
            }

            if (offset > 0)
            {
                return ModContent.RarityType<PulsatingPurple>();
            }

            return Type;
        }
    }
}
