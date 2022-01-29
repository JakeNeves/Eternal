using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class JakesHat : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jake's Hat");
            Tooltip.SetDefault("'Great for impersonating Eternal Mod Devs'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.vanity = true;
	        item.value = Item.buyPrice(platinum: 1, gold: 30);
        }

    }
}
