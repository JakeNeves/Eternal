using Eternal.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Placeable.Furniture
{
    public class CosmicToilet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Seriously? THIS is what you " +
                             "\nused the Cosmonium Fragments for?");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 30;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.buyPrice(platinum: 1);
            item.createTile = ModContent.TileType<Tiles.Furniture.CosmicToilet>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkBlue;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Tiles.AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<StellarAlloy>(), 6);
            recipe.AddIngredient(ModContent.ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemID.Toilet);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
