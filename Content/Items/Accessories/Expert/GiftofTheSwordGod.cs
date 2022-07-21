using Eternal.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class GiftofTheSwordGod : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gift of The Sword God (NYI)");
            Tooltip.SetDefault("Taking damage will call upon the Ark of Imperious to aid you in combat" +
                             "\n'The Ark of Imperious' Gift of Courage'");

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
    }
}
