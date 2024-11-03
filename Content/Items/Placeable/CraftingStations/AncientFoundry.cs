using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class AncientFoundry : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 108;
            Item.height = 58;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 2, gold: 30);
            Item.createTile = ModContent.TileType<Tiles.CraftingStations.AncientFoundry>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 60)
                .AddIngredient(ModContent.ItemType<ArkaniumScrap>(), 50)
                .AddIngredient(ModContent.ItemType<StarquartzCrystalCluster>(), 60)
                .AddIngredient(ModContent.ItemType<ArkiumQuartzCrystalCluster>(), 50)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>(), 200)
                .AddIngredient(ModContent.ItemType<Starforge>())
                .AddIngredient(ItemID.LunarCraftingStation)
                .AddRecipeGroup("eternal:adamantiteForges")
                .Register();
        }
    }
}
