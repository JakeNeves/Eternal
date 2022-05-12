using Eternal.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    public class AncientForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft hyper-teir items");
        }

        public override void SetDefaults()
        {
            item.width = 80;
            item.height = 60;
            item.maxStack = 999;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.buyPrice(platinum: 2, gold: 30);
            item.createTile = TileType<Tiles.AncientForge>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CometiteBar>(), 10);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 200);
            recipe.AddIngredient(ItemType<Astragel>(), 30);
            recipe.AddIngredient(ItemType<InterstellarSingularity>(), 15);
            recipe.AddIngredient(ItemType<Starforge>());
            recipe.AddIngredient(ItemID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddIngredient(ItemType<CometiteBar>(), 10);
            altrecipe.AddIngredient(ItemType<StarmetalBar>(), 200);
            altrecipe.AddIngredient(ItemType<Astragel>(), 30);
            altrecipe.AddIngredient(ItemType<InterstellarSingularity>(), 15);
            altrecipe.AddIngredient(ItemType<Starforge>());
            altrecipe.AddIngredient(ItemID.LunarCraftingStation);
            altrecipe.AddIngredient(ItemID.TitaniumForge);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        }
    }
}
