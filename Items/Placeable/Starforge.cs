using Eternal.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    public class Starforge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft starmetal and cometite-teir items");
        }

        public override void SetDefaults()
        {
            item.width = 80;
            item.height = 48;
            item.maxStack = 999;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 20);
            item.createTile = TileType<Tiles.Starforge>();
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
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddIngredient(ItemID.MythrilAnvil);
            recipe.AddIngredient(ItemID.LunarBar, 4);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.CrystalBall);
            altrecipe.AddIngredient(ItemID.OrichalcumAnvil);
            altrecipe.AddIngredient(ItemID.LunarBar, 4);
            altrecipe.AddIngredient(ItemType<StarmetalBar>(), 8);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        } 
    }
}
