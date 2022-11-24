using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class FrostblightCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A mere fragment of the Eternal frostborn spirits'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 58;
            Item.value = Item.buyPrice(platinum: 1, gold: 30);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<FrostblightCrystal>(), 4)
                .AddIngredient(ModContent.ItemType<GalaciteBar>(), 12)
                .Register();
        }
    }
}
