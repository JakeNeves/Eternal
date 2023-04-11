using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class TritaniumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'A metallic alloy of pure starpower'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(2, 8));
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>())
                .AddIngredient(ItemID.TitaniumBar)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
