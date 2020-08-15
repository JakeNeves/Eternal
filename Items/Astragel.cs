using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class Astragel : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Not to be confused with normal Gel or Astrageldeon'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(platinum: 1, gold: 30);
            item.rare = ItemRarityID.Purple;
            item.maxStack = 999;
        }
    }
}
