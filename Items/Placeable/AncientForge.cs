using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class AncientForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to craft ANYTHING you want.");
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
            item.createTile = TileType<Tiles.AncientForge>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.WorkBench);
            recipe.AddIngredient(ItemID.Furnace);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddIngredient(ItemID.AlchemyTable);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.TinkerersWorkshop);
            recipe.AddIngredient(ItemType<SoulofTwilight>(), 3);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 5);
            recipe.AddIngredient(ItemType<SignalumBar>(), 5);
            recipe.AddIngredient(ItemType<Rudanium>(), 10);
            recipe.AddIngredient(ItemType<AncientCrystalineScrap>(), 10);
            recipe.AddIngredient(ItemType<AncientDust>(), 10);
            recipe.AddIngredient(ItemType<Carmanite>(), 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        } 
    }
}
