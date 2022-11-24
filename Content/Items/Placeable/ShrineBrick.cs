using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Content.Items.Placeable
{
    public class ShrineBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrine Stone");
            Tooltip.SetDefault("Mined with a Lunar Pickaxe or Higher..." +
                "\n'Those were the days...'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.White;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = TileType<Tiles.ShrineBrick>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ShrineBrickWall>(), 4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
