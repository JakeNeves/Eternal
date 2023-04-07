using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class SebastionsCyberChassis : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sebastion's Cyber Chassis");
            // Tooltip.SetDefault("'It looks rather hollow and empty on the inside...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 30;
            Item.value = Item.buyPrice(platinum: 25);
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<GalaciteGemstone>(), 25)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 100)
                .AddIngredient(ModContent.ItemType<ConcintratedHardStone>(), 20)
                // .AddIngredient(ModContent.ItemType<NaquadahBar>(), 5)
                .AddRecipeGroup("eternal:copperBars", 75)
                .AddTile(ModContent.TileType<Reconstructatorium>())
                .Register();
        }
    }
}
