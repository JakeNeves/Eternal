using Eternal.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Materials
{
    public class IgniumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<IgniumOre>())
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
