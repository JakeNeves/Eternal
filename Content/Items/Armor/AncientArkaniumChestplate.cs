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
    public class AncientArkaniumChestplate : ModItem
    {
        public static readonly int MaxHealthBonus = 80;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxHealthBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(platinum: 2);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += MaxHealthBonus;
        }
    }
}
