using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    class DivineBarrier : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Provides a barrier both in front and behind you.");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(gold: 15, silver: 75);
            item.rare = 8;
            item.accessory = true;
            item.defense = 25;
            item.lifeRegen = 20;
        }
    }
}
