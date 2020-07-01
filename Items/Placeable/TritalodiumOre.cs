using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class TritalodiumOre : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.sellPrice(silver: 30, copper: 75);
            item.rare = 2;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = TileType<Tiles.TritalodiumOre>();
            item.maxStack = 999;
        }
    }
}
