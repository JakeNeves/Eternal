﻿using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Placeable
{
    public class CometiteBrick : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.White;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.Decorative.CometiteBrick>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Tiles.Starforge>());
            recipe.AddIngredient(ModContent.ItemType<CometiteOre>());
            recipe.AddIngredient(ItemID.StoneBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}