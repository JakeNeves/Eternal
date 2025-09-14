using Eternal.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class GiftofTheSwordGod : ModItem
    {
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("Gift of The Sword God (NYI)");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Taking damage will call upon the Ark of Imperious to aid you in combat" +
                                                                            "\n'The Ark of Imperious' Gift of Courage'");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 38;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.value = Item.sellPrice(gold: 12);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.GiftofTheSwordGod = true;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
