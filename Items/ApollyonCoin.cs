using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    public class ApollyonCoin : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used for spending with the Apollyon");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }

    }
}
