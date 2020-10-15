using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Items;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Tools
{
    public class MilkyWayPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can Mine Cometite");
        }

        public override void SetDefaults()
        {
            item.tileBoost = 4;
            item.damage = 80;
            item.melee = true;
            item.width = 54;
            item.height = 46;
            item.useTime = 10;
            item.useAnimation = 15;
            item.pick = 225;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.value = Item.buyPrice(gold: 30, silver: 75);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
