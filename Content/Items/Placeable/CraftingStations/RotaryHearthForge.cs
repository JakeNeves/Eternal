using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class RotaryHearthForge : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 112;
            Item.height = 128;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(gold: 90);
            Item.createTile = ModContent.TileType<Tiles.CraftingStations.RotaryHearthForge>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 12)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 20)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 16)
                .AddTile(ModContent.TileType<Tiles.CraftingStations.Starforge>())
                .Register();
        }
    }
}
