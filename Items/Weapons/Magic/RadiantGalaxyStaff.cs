using Eternal.Projectiles.Weapons.Magic;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Magic
{
    public class RadiantGalaxyStaff : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Rainbow Laser\n'Vibrant, Pure, Obliterating... DESTRUCTION!'");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 12000;
            item.noMelee = true;
            item.magic = true;
            item.channel = true;
            item.mana = 90;
            item.rare = ItemRarityID.Red;
            item.width = 84;
            item.height = 84;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = ProjectileType<RadiantGalaxyStaffProjectile>();
            item.value = Item.sellPrice(platinum: 10);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkBlue;
                }
            }
        }

    }
}
