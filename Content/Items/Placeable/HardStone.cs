using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class HardStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Hardened rock, formed from giant clusters of many minerals'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.Blue;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.HardStone>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Materials.HardStoneFragment>(), 18)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
