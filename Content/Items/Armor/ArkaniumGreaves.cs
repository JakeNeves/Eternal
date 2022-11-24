using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ArkaniumGreaves : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+27% increased movement speed");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(platinum: 2);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.27f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 12)
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 18)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
