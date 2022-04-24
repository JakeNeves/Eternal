using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Potions
{
    public class PristineHealingPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Red;
            item.healLife = 350;
            item.potion = true;
            item.value = Item.sellPrice(gold: 20);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<Astragel>());
            recipe.AddIngredient(ItemID.SuperHealingPotion);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }
    }
}
