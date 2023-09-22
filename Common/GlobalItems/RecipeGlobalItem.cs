using Eternal.Common.Configurations;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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

                if (recipe.TryGetResult(ItemID.SuspiciousLookingEye, out Item result))
                {
                    if (recipe.TryGetIngredient(ItemID.Lens, out Item ingredient))
                    {
                        ingredient.stack = 10;
                    }
                }
            }
        }
    }
}
