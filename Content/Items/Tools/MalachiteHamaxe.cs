﻿using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class MalachiteHamaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.axe = 50;
            Item.hammer = 75;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 1, silver: 3);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MalachiteSheets>(), 9)
                .AddIngredient(ModContent.ItemType<IesniumBar>(), 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
