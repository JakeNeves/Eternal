﻿using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Potions
{
    public class FineRedWine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Some of the emissaries say, this is refered as the 'blood' of the emperor...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(gold: 16);
            Item.buffType = BuffID.Tipsy;
            Item.buffTime = 510;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Tipsy, 510);
        }
    }
}