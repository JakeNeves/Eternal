using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;
using System.Collections.Generic;

namespace Eternal.Items.Materials
{
    public class CometiteCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A crystaline shard of pristine starpower'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 10));
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
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
            recipe.AddIngredient(ItemType<ApparitionalMatter>());
            recipe.AddIngredient(ItemType<InterstellarSingularity>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
