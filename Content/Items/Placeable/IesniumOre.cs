﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class IesniumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragment of otherworldly energy\n'Etherial energy radiates from the cluster'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.IesniumOre>();
            Item.maxStack = 999;
        }
    }
}