using Eternal.Common.Configurations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalItems
{
    public class RecipeGlobalItem : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.recipeChanges;
        }

        public override void AddRecipes()
        {
            Recipe rodRecipe = Recipe.Create(ItemID.RodofDiscord);
            rodRecipe.AddIngredient(ItemID.PixieDust, 20)
                .AddIngredient(ItemID.UnicornHorn, 2)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddCondition(Condition.InHallow)
                .Register();

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ItemID.SuspiciousLookingEye, out Item suspiciousLookingEye))
                {
                    if (recipe.TryGetIngredient(ItemID.Lens, out Item lens))
                    {
                        lens.stack = 10;
                    }
                }

                /*if (recipe.TryGetResult(ItemID.MechanicalEye, out Item mechanicalEye))
                {
                    recipe.DisableRecipe();

                    recipe.AddTile(TileID.MythrilAnvil)
                        .AddIngredient(ItemID.Lens, 3)
                        .AddRecipeGroup("eternal:copperBars", 5)
                        .AddRecipeGroup("eternal:ironBars", 5)
                        .AddIngredient(ItemID.SoulofLight, 6)
                        .Register();
                }

                if (recipe.TryGetResult(ItemID.MechanicalSkull, out Item mechanicalSkull))
                {
                    recipe.DisableRecipe();

                    recipe.AddTile(TileID.MythrilAnvil)
                        .AddIngredient(ItemID.Bone, 30)
                        .AddRecipeGroup("eternal:copperBars", 5)
                        .AddRecipeGroup("eternal:ironBars", 5)
                        .AddIngredient(ItemID.SoulofLight, 3)
                        .AddIngredient(ItemID.SoulofNight, 3)
                        .Register();
                }

                if (recipe.TryGetResult(ItemID.MechanicalWorm, out Item mechanicalWorm))
                {
                    recipe.DisableRecipe();

                    recipe.AddTile(TileID.MythrilAnvil)
                        .AddRecipeGroup("eternal:rottenChunks", 6)
                        .AddRecipeGroup("eternal:copperBars", 5)
                        .AddRecipeGroup("eternal:ironBars", 5)
                        .AddIngredient(ItemID.SoulofNight, 6)
                        .Register();
                }*/
            }
        }
    }
}
