using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials.Elementalblights
{
    public class ConcintratedInfernoblight : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Concintrated energy of fire and lava creatures'");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 34;
            item.value = Item.sellPrice(gold: 1);
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
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<InfernoblightCrystal>());
            recipe.AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
