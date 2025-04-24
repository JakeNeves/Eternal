using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Decorative
{
    public class GehennaBrickWall : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<Walls.GehennaBrickWall>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient(ModContent.ItemType<GehennaBrick>())
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}