﻿using Eternal.Dusts;
using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class Cosmigrim : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Now you can slice through the tiny pesky meteors yourself!'");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 52;
            item.melee = true;
            item.rare = ItemRarityID.Red;
            item.damage = 220;
            item.useAnimation = 20;
            item.useTime = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10;
            item.autoReuse = true;
            item.UseSound = SoundID.Item15;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType<Starmetal>());
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<CometiteBar>(), 5);
            recipe.AddIngredient(ItemType<GalaxianPlating>(), 10);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 100);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}