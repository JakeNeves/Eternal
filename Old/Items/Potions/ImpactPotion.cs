﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Potions
{
    public class ImpactPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Your attacks deal 25% extra damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.consumable = true;
            item.maxStack = 30;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 16;
            item.useTime = 16;
            item.useTurn = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(gold: 20);
            item.buffType = ModContent.BuffType<Buffs.Impact>();
            item.buffTime = 5200;
            item.UseSound = SoundID.Item3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Bottles);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Daybloom, 4);
            recipe.AddIngredient(ItemID.Obsidian, 2);
            recipe.AddIngredient(ItemID.TissueSample, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.Bottles);
            altrecipe.AddIngredient(ItemID.BottledWater);
            altrecipe.AddIngredient(ItemID.Daybloom, 4);
            altrecipe.AddIngredient(ItemID.Obsidian, 2);
            altrecipe.AddIngredient(ItemID.ShadowScale, 3);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        }
    }
}