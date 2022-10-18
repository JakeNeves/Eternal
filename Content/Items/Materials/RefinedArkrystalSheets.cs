using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Materials
{
    public class RefinedArkrystalSheets : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Durable, yet so shiny...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(platinum: 6);
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RawArkrystal>(), 6)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
