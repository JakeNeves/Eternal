using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Alchemaster
{
    public class AlchemasterTestItem : AlchemasterItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Test Item\nThrows an ale, nothing special...");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 1000;
            item.crit = 100;
            item.knockBack = 0f;
            item.rare = ItemRarityID.Red;
            item.width = 28;
            item.height = 36;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shoot = ProjectileID.Ale;
            item.shootSpeed = 3f;
            item.noMelee = true;
            item.noUseGraphic = true;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit = Main.LocalPlayer.rangedCrit - Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].crit + Main.HoverItem.crit;
            base.GetWeaponCrit(player, ref crit);
        }

    }
}
