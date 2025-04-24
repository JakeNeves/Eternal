using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Placeable.Furniture
{
    public class OminaquaditeChair : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 1);
            Item.createTile = ModContent.TileType<Tiles.Furniture.OminaquaditeChair>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<OminaquaditeBar>(), 6)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 12)
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 6)
                .Register();
        }
    }
}
