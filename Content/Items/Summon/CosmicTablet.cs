using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class CosmicTablet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used at the Altar of Cosmic Desire" +
                             "\nSummons the Cosmic Emperor" +
                             "\n'Used to worship the emperor, many times to your heart's desire!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 52;
            Item.rare = ModContent.RarityType<Magenta>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 12)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 16)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 20)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
