using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    public class InterstellarSingularity : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The Essance of Cosmic Beings'");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 1);
            item.maxStack = 999;
        }

    }
}
