using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class WeaponsGradeNaquadahAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = Item.sellPrice(platinum: 60);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RefinedExathium>())
                .AddIngredient(ModContent.ItemType<MeldedExathiumShard>())
                .AddIngredient(ModContent.ItemType<NaquadahBar>())
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>())
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
