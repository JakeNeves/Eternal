﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class MalachiteSheets : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 10, silver: 95);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MalachiteShard>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
