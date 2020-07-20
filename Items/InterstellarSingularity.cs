using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class InterstellarSingularity : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The Essance of Cosmic Beings'");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 20;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 1);
            item.maxStack = 99;
        }

    }
}
