using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class AtomReconstructor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Reconstructs materials atom by atom to create something new...");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 40;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 20);
            item.createTile = TileType<Tiles.AtomReconstructor>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddIngredient(ItemID.SoulofLight, 30);
            recipe.AddIngredient(ItemID.SoulofNight, 30);
            recipe.AddIngredient(ItemID.PixieDust, 15);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
