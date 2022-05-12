using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class StellarAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A Gift from the Cosmic Champion\n'So This is True Starmetal...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = Item.buyPrice(platinum: 15);
            item.rare = ItemRarityID.Red;
            item.maxStack = 99;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(5, 35, 215);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<InterstellarMetal>());
            recipe.AddIngredient(ItemType<InterstellarSingularity>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
