using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class Carmanite : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Blood Droplets, Petrified and Bewiched in a bizzare form \n'Essance of Carmanite Creatures'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(silver: 1);
            item.maxStack = 999;
        }

    }
}
