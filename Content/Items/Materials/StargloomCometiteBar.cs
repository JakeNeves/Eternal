﻿using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class StargloomCometiteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The strongest starmetal that withstands a field of asteriods'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>())
                .AddIngredient(ModContent.ItemType<CometiteCrystal>())
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>())
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}