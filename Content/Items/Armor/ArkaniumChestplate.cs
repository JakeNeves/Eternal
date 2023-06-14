using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ArkaniumChestplate : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+160 max life");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.value = Item.sellPrice(platinum: 2);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 48;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 160;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 60)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 20)
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 96)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
