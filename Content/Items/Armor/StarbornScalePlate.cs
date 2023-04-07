using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class StarbornScalePlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+25 increased max life");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 42;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 24)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
