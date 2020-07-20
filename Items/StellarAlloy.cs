using Eternal.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items
{
    class StellarAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A Gift from the Cosmic Champion\n'So This is True Starmetal...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = Item.buyPrice(platinum: 15);
            item.rare = ItemRarityID.Red;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<InterstellarMetal>());
            recipe.AddIngredient(ItemType<InterstellarSingularity>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
