using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class ScorchedMetal : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Incinerius' Finest Metal");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(silver: 10);
            item.maxStack = 99;
        }

    }
}
