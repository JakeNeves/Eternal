using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;
using System.Collections.Generic;

namespace Eternal.Items.Materials
{
    public class CometiteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A metallic alloy of pure starpower'");

            ItemID.Sets.ItemNoGravity[item.type] = true;

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
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
            recipe.AddTile(TileType<Tiles.Starforge>());
            recipe.AddIngredient(ItemType<StarmetalBar>());
            recipe.AddIngredient(ItemType<InterstellarSingularity>());
            recipe.AddIngredient(ItemType<Items.Placeable.CometiteOre>());
            recipe.AddIngredient(ItemType<Astragel>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
