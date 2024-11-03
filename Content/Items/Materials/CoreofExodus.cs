using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class CoreofExodus : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientFoundry>())
                .AddIngredient(ModContent.ItemType<FrostblightSapphire>())
                .AddIngredient(ModContent.ItemType<InfernoblightAmber>())
                .AddIngredient(ModContent.ItemType<ThunderblightJade>())
                .AddIngredient(ModContent.ItemType<ShiftblightAmethyst>())
                .Register();
        }
    }
}
