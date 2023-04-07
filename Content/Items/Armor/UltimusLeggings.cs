using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class UltimusLeggings : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+40% increased movement speed");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 14;
            Item.value = Item.sellPrice(platinum: 4);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 1.40f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientForge>())
                .AddIngredient(ModContent.ItemType<ArkaniumGreaves>())
                .AddIngredient(ModContent.ItemType<StarbornGreaves>())
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 8)
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 12)
                .Register();
        }
    }
}
