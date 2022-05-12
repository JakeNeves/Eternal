using Eternal.Projectiles.Weapons.Throwing;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Throwing
{
    public class ApparitionalDisk : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows an Apparitional Disk That Flies Upwards");
        }
        public override void SetDefaults()
        {
            item.damage = 220;
            item.ranged = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 5;
            item.useAnimation = 5;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 25;
            item.value = Item.buyPrice(platinum: 5);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 20.4f;
            item.shoot = ProjectileType<ApparitionalDiskThrowing>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

    }
}
