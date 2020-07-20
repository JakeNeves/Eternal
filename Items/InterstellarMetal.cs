using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class InterstellarMetal : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Something is missing with this bar...'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 10);
            item.maxStack = 99;
        }

    }
}
