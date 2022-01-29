using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Materials;
using System.Collections.Generic;
using Eternal.Tiles;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ArkaniumChestplate : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+160 max life");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 22;
            item.value = Item.sellPrice(platinum: 2);
            item.rare = ItemRarityID.Red;
            item.defense = 48;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 160;
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
            recipe.AddIngredient(ModContent.ItemType<BrokenLabrynthSword>(), 12);
            recipe.AddIngredient(ModContent.ItemType<ArkaniumCompound>(), 48);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
