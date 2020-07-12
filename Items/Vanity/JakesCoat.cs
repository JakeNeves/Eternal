using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    class JakesCoat : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jake's Coat");
            Tooltip.SetDefault("'Great for impersonating Eternal Mod Devs'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 22;
            item.vanity = true;
        }

    }
}
