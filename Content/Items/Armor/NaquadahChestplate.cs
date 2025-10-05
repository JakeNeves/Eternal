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
    public class NaquadahChestplate : ModItem
    {
        public static readonly int MaxHealthBonus = 200;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxHealthBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(platinum: 30);
            Item.rare = ModContent.RarityType<Maroon>();
            Item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += MaxHealthBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 16)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 32)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
