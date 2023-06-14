using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class NaquadahBar : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("'A material made of otherworldly rift-like scrap...'");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = Item.sellPrice(platinum: 20);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RawNaquadah>(), 2)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
