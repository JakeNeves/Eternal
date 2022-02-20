using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Materials;
using System.Collections.Generic;
using Eternal.Tiles;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class UltimusLeggings : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+40% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 14;
            item.value = Item.sellPrice(platinum: 4);
            item.rare = ItemRarityID.Red;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.40f;
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
            recipe.AddIngredient(ModContent.ItemType<ArkaniumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StarbornGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 16);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
