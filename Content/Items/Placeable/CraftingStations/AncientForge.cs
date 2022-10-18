using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class AncientForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft hyper-teir items");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 60;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 2, gold: 30);
            Item.createTile = ModContent.TileType<Content.Tiles.CraftingStations.AncientForge>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 10)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 200)
                .AddIngredient(ModContent.ItemType<Astragel>(), 30)
                .AddIngredient(ModContent.ItemType<InterstellarSingularity>(), 15)
                .AddIngredient(ModContent.ItemType<Starforge>())
                .AddIngredient(ItemID.LunarCraftingStation)
                .AddIngredient(ItemID.AdamantiteForge)
                .Register();

            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 10)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 200)
                .AddIngredient(ModContent.ItemType<Astragel>(), 30)
                .AddIngredient(ModContent.ItemType<InterstellarSingularity>(), 15)
                .AddIngredient(ModContent.ItemType<Starforge>())
                .AddIngredient(ItemID.LunarCraftingStation)
                .AddIngredient(ItemID.TitaniumForge)
                .Register();
        }
    }
}
