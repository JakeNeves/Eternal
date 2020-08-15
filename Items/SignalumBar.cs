using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class SignalumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Looks like Paladium...");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(gold: 5, silver: 90);
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.CopperBar);
            recipe.AddIngredient(ItemID.HallowedBar);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipealt = new ModRecipe(mod);
            recipealt.AddTile(TileID.MythrilAnvil);
            recipealt.AddIngredient(ItemID.TinBar);
            recipealt.AddIngredient(ItemID.HallowedBar);
            recipealt.SetResult(this);
            recipealt.AddRecipe();
        }

    }
}
