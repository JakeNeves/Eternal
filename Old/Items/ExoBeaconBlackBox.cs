using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    public class ExoBeaconBlackBox : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon Blackbox");
            Tooltip.SetDefault("Used at the Exo Beacon" +
                              "\nContacts Dr. Sebastion Kox");
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 36;
            item.value = Item.sellPrice(platinum: 1, gold: 5);
            item.rare = ItemRarityID.Red;
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
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ItemID.LunarBar, 3);
            recipe.AddIngredient(ItemID.Wire, 12);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
