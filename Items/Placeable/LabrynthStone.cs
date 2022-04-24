using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    public class LabrynthStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shrine Stone");
            Tooltip.SetDefault("Mined with a Lunar Pickaxe or Higher..." +
                "\n'Those were the days...'");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.White;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = TileType<Tiles.LabrynthStone>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ItemType<LabrynthStoneWall>(), 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
