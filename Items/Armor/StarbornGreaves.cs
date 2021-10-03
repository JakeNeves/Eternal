using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Materials;
using System.Collections.Generic;
using Eternal.Tiles;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class StarbornGreaves : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+18% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(gold: 15);
            item.rare = ItemRarityID.Red;
            item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.18f;
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
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
