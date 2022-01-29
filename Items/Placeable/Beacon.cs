using Eternal.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Placeable
{
    public class Beacon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Beacon");
            Tooltip.SetDefault("Used to call upon powerful machinery");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 44;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 15);
            item.createTile = ModContent.TileType<Tiles.Interactive.Beacon>();
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
            recipe.AddIngredient(ModContent.ItemType<BeaconChassis>());
            recipe.AddIngredient(ModContent.ItemType<BeaconCoreStablizer>());
            recipe.AddIngredient(ModContent.ItemType<BeaconEnergyFuse>());
            recipe.AddIngredient(ModContent.ItemType<BeaconIOCard>());
            recipe.AddIngredient(ModContent.ItemType<BeaconTXRXSignalController>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
