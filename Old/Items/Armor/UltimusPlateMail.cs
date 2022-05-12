using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class UltimusPlateMail : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+200 max life");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 28;
            item.value = Item.sellPrice(platinum: 4);
            item.rare = ItemRarityID.Red;
            item.defense = 96;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 200;
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
            recipe.AddIngredient(ModContent.ItemType<ArkaniumChestplate>());
            recipe.AddIngredient(ModContent.ItemType<StarbornScalePlate>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 24);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
