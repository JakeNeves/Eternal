using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Placeable;

namespace Eternal.Content.Items.Materials
{
    public class ConcintratedHardStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The heaviest form of rock, formed with impossibly heavy pressure...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 42;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 12);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HardStone>(), 18)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
