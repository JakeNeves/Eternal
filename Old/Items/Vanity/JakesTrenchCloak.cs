using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Front, EquipType.Back)]
    class JakesTrenchCloak : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jake's Trench Cloak");
            Tooltip.SetDefault("'Great for impersonating Eternal Devs'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
            item.accessory = true;
            item.vanity = true;
        }

    }
}
