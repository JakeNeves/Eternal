using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Accessories
{
    public class FierceDominatorEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fierce Dominator Emblem");
            Tooltip.SetDefault("45% increased melee damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ModContent.RarityType<Teal>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.45f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ItemID.WarriorEmblem)
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 16)
                .Register();
        }
    }
}
