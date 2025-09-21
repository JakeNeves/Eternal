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
    public class UltimusLeggings : ModItem
    {

        public static readonly int MoveSpeedBonus = 40;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
            player.moveSpeed += MoveSpeedBonus / 100f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<AncientFoundry>())
                .AddIngredient(ModContent.ItemType<ArkaniumGreaves>())
                .AddIngredient(ModContent.ItemType<StarbornGreaves>())
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 8)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 12)
                .Register();
        }
    }
}
