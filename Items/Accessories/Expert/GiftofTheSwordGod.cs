using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories.Expert
{
    public class GiftofTheSwordGod : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gift of The Sword God");
            Tooltip.SetDefault("Melee weapons fire a spirit of the Ark of Imperious" +
                             "\n'The Ark of Imperious' Gift of Courage'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.rare = ItemRarityID.Expert;
            item.expert = true;
            item.value = Item.sellPrice(gold: 12);
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            EternalPlayer.AoIGift = true;
        }
    }
}
