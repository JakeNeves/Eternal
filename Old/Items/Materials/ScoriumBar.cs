﻿using Eternal.Items.Materials.Elementalblights;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class ScoriumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Sizzlin' Hot Material of The Underworld.'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 12));
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 5, silver: 90);
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddIngredient(ModContent.ItemType<Placeable.ScoriumOre>());
            recipe.AddIngredient(ModContent.ItemType<InfernoblightCrystal>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}