using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Legs)]
    public class JakesKicks : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jake's Kicks");
            Tooltip.SetDefault("'Great for impersonating Eternal Devs'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.vanity = true;
            item.value = Item.buyPrice(platinum: 1, gold: 30);
        }

    }
}
