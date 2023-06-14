using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class Teal : ModRarity
    {
        public override Color RarityColor => new Color(71, 245, 169);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            {
                return ItemRarityID.Purple;
            }

            if (offset > 0)
            {
                return ModContent.RarityType<Magenta>();
            }

            return Type;
        }
    }
}
