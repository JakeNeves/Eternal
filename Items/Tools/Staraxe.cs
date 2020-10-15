using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Tools
{
    class Staraxe : ModItem
    {

        public override void SetDefaults()
        {
            item.tileBoost = 12;
            item.damage = 200;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 10;
            item.useAnimation = 15;
            item.axe = 200;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 12;
            item.value = Item.buyPrice(gold: 30, silver: 75);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<CometiteBar>(), 4);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 5);
            recipe.AddIngredient(ItemType<GalaxianPlating>(), 4);
            recipe.AddIngredient(ItemType<Astragel>(), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
