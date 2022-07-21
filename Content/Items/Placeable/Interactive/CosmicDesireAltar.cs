using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Interactive
{
    public class CosmicDesireAltar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Altar of Cosmic Desire");
            Tooltip.SetDefault("Used to worship the Cosmic Emperor");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 48;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 6);
            Item.createTile = ModContent.TileType<Content.Tiles.Interactive.CosmicDesireAltar>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 8)
                .AddIngredient(ModContent.ItemType<CometiteOre>(), 20)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 30)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
