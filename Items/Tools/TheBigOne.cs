using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Tools
{
    class TheBigOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A Very Big Axe\n'Someone resprite this please...'");
        }

        public override void SetDefaults()
        {
            item.damage = 10000;
            item.melee = true;
            item.width = 170;
            item.height = 164;
            item.useTime = 50;
            item.useAnimation = 50;
            item.axe = 100;
            item.hammer = 200;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 0;
            item.value = Item.buyPrice(platinum: 1);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
