using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Content.Items.Placeable.Decorative
{
    public class ScorchingBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shrine Stone");
            // Tooltip.SetDefault("\n'Hot'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.White;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.DefaultToPlaceableTile(TileType<Tiles.ScorchingBrick>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HardStone>())
                .AddIngredient(ItemID.Hellstone)
                .AddIngredient(ItemID.GrayBrick)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
