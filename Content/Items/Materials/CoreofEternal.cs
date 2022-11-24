using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class CoreofEternal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of Eternal");
            Tooltip.SetDefault("'A mere fragment of the Eternal Primordials'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 58;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<FrostblightCore>())
                .AddIngredient(ModContent.ItemType<InfernoblightCore>())
                .AddIngredient(ModContent.ItemType<ThunderblightCore>())
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 12)
                .Register();
        }
    }
}
