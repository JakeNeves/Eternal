﻿using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
{
    public class CometGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("60% increased melee damage" +
                               "\n17% increased melee speed" +
                               "\n'The comets are now in the palm of your hand'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            EternalGlobalProjectile.cometGauntlet = true;

            player.meleeDamage += 0.6f;
            player.meleeSpeed += 0.17f;
            player.meleeDamageMult += 0.3f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
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
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 20);
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 40);
            recipe.AddIngredient(ModContent.ItemType<InterstellarSingularity>(), 10);
            recipe.AddIngredient(ItemID.FireGauntlet);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}