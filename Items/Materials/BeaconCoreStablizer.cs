using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class BeaconCoreStablizer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon Core Stablizer");
            Tooltip.SetDefault("'Used for Stablizing the Cores that is placed on the Exo Beacon when installed onto a Beacon Chassis'");
        }

        public override void SetDefaults()
        {
            item.width = 38;
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
            recipe.AddIngredient(ItemID.HallowedBar, 3);
            recipe.AddIngredient(ItemID.Wire, 6);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
