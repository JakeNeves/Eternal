﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.ID;

namespace Eternal.Content.Items.Materials
{
    public class ArkiumQuartzPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<RefinedExathium>();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(platinum: 6);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkiumQuartzCrystalCluster>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
