using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Vanity
{
    public class TheEternalCursor : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Causes your mouse cursor to shift from various shades of red");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.accessory = true;
            Item.vanity = true;
        }

        public override void UpdateVanity(Player player)
        {
            AccessorySystem.hasTheEternalCursor = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CursorofTheCosmos>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>())
                .AddTile(ModContent.TileType<Reconstructatorium>())
                .Register();
        }
    }
}
