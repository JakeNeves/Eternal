using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class Dawnstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Bestowed with dwarvern magic...'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = Item.buyPrice(silver: 50);
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddIngredient(ModContent.ItemType<ScorchedGrimstoneCompound>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
