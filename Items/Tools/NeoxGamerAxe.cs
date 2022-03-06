using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Tools
{
    public class NeoxGamerAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("N30X Gamer Axe");
            Tooltip.SetDefault("'The axe that eveolved into it's gamer form'");
        }

        public override void SetDefaults()
        {
            item.tileBoost = 8;
            item.damage = 2000;
            item.melee = true;
            item.width = 46;
            item.height = 46;
            item.useTime = 12;
            item.useAnimation = 12;
            item.axe = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 0;
            item.value = Item.buyPrice(platinum: 1);
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
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<NeoxCore>(), 6);
            recipe.AddIngredient(ItemType<CoreofEternal>(), 12);
            recipe.AddIngredient(ItemID.MeteorHamaxe);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
