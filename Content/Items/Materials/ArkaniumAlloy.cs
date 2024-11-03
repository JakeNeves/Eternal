using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Rarities;
using Terraria.ID;

namespace Eternal.Content.Items.Materials
{
    public class ArkaniumAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<MeldedExathiumShard>();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(platinum: 6);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkaniumScrap>(), 2)
                .AddIngredient(ModContent.ItemType<SarosFragment>(), 2)
                .AddIngredient(ModContent.ItemType<StarquartzCrystalCluster>(), 16)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
