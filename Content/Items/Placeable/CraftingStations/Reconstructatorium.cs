using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class Reconstructatorium : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft divine-teir items");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 92;
            Item.height = 54;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(gold: 20);
            Item.createTile = ModContent.TileType<Content.Tiles.CraftingStations.Reconstructatorium>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AncientForge>())
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 48)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 26)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 100)
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 30)
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 250)
                .Register();
        }
    }
}
