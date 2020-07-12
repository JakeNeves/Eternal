using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Legs)]
    class JakesKicks : ModItem
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
        }

    }
}
