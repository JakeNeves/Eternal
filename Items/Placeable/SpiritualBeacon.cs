using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class SpiritualBeacon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Enhances Divine and Mythical Weapons, Accessories, and Tools...");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 40;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 20);
            item.createTile = TileType<Tiles.SpiritualBeacon>();
        }
    }
}
