using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class UltimusPlateMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+200 max life");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.value = Item.sellPrice(platinum: 4);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 96;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 200;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<ArkaniumChestplate>())
                .AddIngredient(ModContent.ItemType<StarbornScalePlate>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 24)
                .Register();
        }
    }
}
