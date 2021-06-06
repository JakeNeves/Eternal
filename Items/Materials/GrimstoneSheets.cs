using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class GrimstoneSheets : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Sheets from the deepest of The Beneath'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 28;
            item.value = Item.buyPrice(platinum: 1, gold: 25, silver: 50);
            item.rare = ItemRarityID.Green;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemType<DepthsDebris>());
            recipe.AddIngredient(ItemType<Items.Placeable.Grimstone>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
