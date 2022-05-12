using Eternal.Items.Materials.Elementalblights;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class CoreofEternal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of Eternal");
            Tooltip.SetDefault("'A mere fragment of the Eternal Primordials'");

            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.value = Item.buyPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
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
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<ConcintratedFrostblight>());
            recipe.AddIngredient(ModContent.ItemType<ConcintratedInfernoblight>());
            recipe.AddIngredient(ModContent.ItemType<ConcintratedThunderblight>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
