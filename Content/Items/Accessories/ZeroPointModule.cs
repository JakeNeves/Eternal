using Terraria;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Accessories
{
    public class ZeroPointModule : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 30;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 = player.statManaMax * 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 8)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 16)
                .Register();
        }
    }
}
