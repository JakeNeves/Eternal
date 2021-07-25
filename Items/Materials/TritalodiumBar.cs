using Eternal.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class TritalodiumBar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = Item.sellPrice(silver: 40, copper: 90);
            item.rare = ItemRarityID.Green;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Furnaces);
            recipe.AddIngredient(ItemType<TritalodiumOre>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
