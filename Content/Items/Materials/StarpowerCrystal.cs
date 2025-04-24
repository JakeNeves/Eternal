using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class StarpowerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Astragel>())
                .AddIngredient(ModContent.ItemType<InterstellarScrapMetal>())
                .AddIngredient(ModContent.ItemType<StarmetalBar>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>())
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
