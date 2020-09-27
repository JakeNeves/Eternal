using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class ArkArrow : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'How do you use tiny little daggers as arrows anyway?'");
        }

        public override void SetDefaults()
        {
            item.damage = 330;
            item.ranged = true;
            item.width = 26;
            item.height = 46;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<ArkArrowProjectile>();
            item.shootSpeed = 18f;
            item.ammo = AmmoID.Arrow;
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
