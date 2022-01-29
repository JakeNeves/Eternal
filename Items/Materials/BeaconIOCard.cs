using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class BeaconIOCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon I/O Card");
            Tooltip.SetDefault("'Used for Controlling the I/O Sensors, Also acts as a logic board'");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 30;
            item.value = Item.buyPrice(gold: 50);
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
            recipe.AddIngredient(RecipeGroupID.IronBar, 6);
            recipe.AddIngredient(ItemID.Wire, 12);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
