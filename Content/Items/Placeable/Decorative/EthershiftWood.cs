﻿using Eternal.Common;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Decorative
{
    public class EthershiftWood : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Decorative.EthershiftWood>();
            Item.maxStack = 9999;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Item.color = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.DeepPink, Color.MediumPurple, Color.DeepPink]);

            return true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Item.color = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.DeepPink, Color.MediumPurple, Color.DeepPink]);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MoteofOminite>())
                .AddIngredient(ItemID.Wood)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
