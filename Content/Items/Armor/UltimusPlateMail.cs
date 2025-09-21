using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class UltimusPlateMail : ModItem
    {
        public static readonly int MaxHealthBonus = 200;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxHealthBonus);

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
            player.statLifeMax2 += MaxHealthBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientFoundry>())
                .AddIngredient(ModContent.ItemType<ArkaniumChestplate>())
                .AddIngredient(ModContent.ItemType<StarbornScalePlate>())
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 24)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 36)
                .Register();
        }
    }
}
