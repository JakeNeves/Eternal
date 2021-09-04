using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class StarbornScalePlate : ModItem
    {

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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ItemType<StarmetalBar>(), 16);
            recipe.AddIngredient(ItemType<CometiteBar>(), 24);
            recipe.AddIngredient(ItemType<GalaxianPlating>(), 12);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
