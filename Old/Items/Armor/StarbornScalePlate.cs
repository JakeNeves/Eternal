using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class StarbornScalePlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25 increased max life");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 26;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Red;
            item.defense = 42;
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

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 24);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
