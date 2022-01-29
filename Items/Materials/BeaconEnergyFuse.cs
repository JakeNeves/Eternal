using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class BeaconEnergyFuse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon Energy Fuse");
            Tooltip.SetDefault("'Used for controlling the amount voltage of the Exo Beacon recieves'");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 28;
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
            recipe.AddIngredient(ItemID.SoulofFlight, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 1);
            recipe.AddIngredient(ItemID.Glass, 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
