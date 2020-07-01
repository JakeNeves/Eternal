using System;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    public class DivineMetalBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Metal constructed from the souls of all creatures, including the Mechanical Trio");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useTime = 19;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.value = 1080;
            item.rare = -12;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("DivineMetalBar"); 
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddIngredient(ItemID.SoulofNight);
            recipe.AddIngredient(ItemID.SoulofFlight);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddIngredient(ItemType<SoulofTwilight>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
