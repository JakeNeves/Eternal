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
    public class ArkaniumChestplate : ModItem
    {
        public static readonly int MaxHealthBonus = 160;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxHealthBonus);

        public override void SetStaticDefaults()
        {
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
            player.statLifeMax2 += MaxHealthBonus;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkaniumAlloy>(), 60)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 20)
                .AddIngredient(ModContent.ItemType<ArkiumQuartzPlating>(), 96)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
