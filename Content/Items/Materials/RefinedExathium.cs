using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Materials
{
    public class RefinedExathium : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = Item.sellPrice(platinum: 6);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RawExathium>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
