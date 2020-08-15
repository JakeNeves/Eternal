using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class Starforge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to craft Starmetal Items");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Red;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = Item.buyPrice(gold: 20);
            item.createTile = TileType<Tiles.Starforge>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddIngredient(ItemID.MythrilAnvil);
            recipe.AddIngredient(ItemID.Lens, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 30);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe altrecipe = new ModRecipe(mod);
            altrecipe.AddTile(TileID.CrystalBall);
            altrecipe.AddIngredient(ItemID.OrichalcumAnvil);
            altrecipe.AddIngredient(ItemID.Lens, 10);
            altrecipe.AddIngredient(ItemID.SoulofLight, 30);
            altrecipe.AddIngredient(ItemID.LunarBar, 5);
            altrecipe.AddIngredient(ItemID.FallenStar, 3);
            altrecipe.SetResult(this);
            altrecipe.AddRecipe();
        } 
    }
}
