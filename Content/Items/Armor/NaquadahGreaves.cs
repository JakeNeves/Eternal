using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class NaquadahGreaves : ModItem
    {
        public static readonly int MoveSpeedBonus = 25;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(platinum: 15);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.defense = 36;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += MoveSpeedBonus / 100f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 8)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 10)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
