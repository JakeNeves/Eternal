using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class ScorchedGrimstoneCompound : ModItem
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
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<GrimstoneSheets>());
            recipe.AddIngredient(ItemType<ScorchedMetal>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
