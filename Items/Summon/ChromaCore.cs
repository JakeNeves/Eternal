using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Eternal.Tiles;
using Eternal.Items.Materials;

namespace Eternal.Items.Summon
{
    public class ChromaCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used at the Exo Beacon" + 
                             "\nCalls upon something powerful");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 19));
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.rare = ItemRarityID.Red;
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
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<FrightCore>());
            recipe.AddIngredient(ModContent.ItemType<MightCore>());
            recipe.AddIngredient(ModContent.ItemType<PiCore>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
