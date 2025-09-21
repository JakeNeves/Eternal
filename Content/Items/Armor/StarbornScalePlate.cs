using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class StarbornScalePlate : ModItem
    {
        public static readonly int MaxHealthBonus = 25;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxHealthBonus);

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 42;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += MaxHealthBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 24)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
