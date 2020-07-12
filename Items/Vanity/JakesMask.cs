using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Face)]
    class JakesMask : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jake's Mask");
            Tooltip.SetDefault("'Great for impersonating Eternal Devs'");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 8;
            item.vanity = true;
            item.accessory = true;
        }

    }
}
