using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Placeable.Furniture
{
    public class CosmicToilet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Seriously? THIS is what you used the Cosmonium Fragments for?" +
                             "\n'This was used by the Cosmic Emperor, sitting on this would be very unacceptable...'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 1);
            Item.createTile = ModContent.TileType<Tiles.Furniture.CosmicToilet>();
        }
    }
}
