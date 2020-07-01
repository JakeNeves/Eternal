using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Tools
{
    class HamaxeofTheRoot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Axe of The Root");
            Tooltip.SetDefault("An axe crafted from Divine Metal and Ancient Crystal, Atomically Constructed... \nA weilder of this axe gains potential power \nto chop and smash trees and walls of there desire...");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
            item.width = 50;
            item.height = 54;
            item.useTime = 10;
            item.useAnimation = 15;
            item.axe = 500;
            item.hammer = 500;
            item.useStyle = 1;
            item.knockBack = 12;
            item.value = Item.buyPrice(gold: 30, silver: 75);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
