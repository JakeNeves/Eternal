using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Throwing
{
    public class ThrowingSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\n'Don't confuse these for arrows.'");
        }

        public override void SetDefaults()
        {
            item.damage = 840;
            item.ranged = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 14;
            item.useAnimation = 14;
            item.width = 26;
            item.height = 46;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<ArkArrowProjectile>();
            item.shootSpeed = 18f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

    }
}
