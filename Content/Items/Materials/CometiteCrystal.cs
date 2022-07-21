using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class CometiteCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A crystaline shard of pristine starpower'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 10));
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Astragel>())
                .AddIngredient(ModContent.ItemType<InterstellarSingularity>())
                .AddIngredient(ModContent.ItemType<StarmetalBar>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>())
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
