using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class AncientForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft hyper-teir items");
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 60;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 2, gold: 30);
            Item.createTile = ModContent.TileType<Content.Tiles.CraftingStations.AncientForge>();
        }
    }
}
