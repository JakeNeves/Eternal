using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class BeaconTXRXSignalController : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon TX/RX Signal Controller");
            Tooltip.SetDefault("'Used for decoding and reading Recieving Signals and encoding and sending Transmission Signals'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
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
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.Wire, 6);
            recipe.AddIngredient(ItemID.Lens, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
