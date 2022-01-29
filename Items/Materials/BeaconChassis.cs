using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class BeaconChassis : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon Chassis");
            Tooltip.SetDefault("'The base of the Exo Beacon'");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 44;
            item.value = Item.buyPrice(silver: 30);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Tiles.Starforge>());
            recipe.AddIngredient(RecipeGroupID.IronBar, 16);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddIngredient(ModContent.ItemType<Astragel>(), 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
