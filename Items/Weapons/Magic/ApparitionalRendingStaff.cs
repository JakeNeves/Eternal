using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class ApparitionalRendingStaff : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Apparitional Disks that fly upwards" +
                "\n'Not an Actual Rending Staff'");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 220;
            item.magic = true;
            item.mana = 20;
            item.width = 44;
            item.height = 46;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = Item.sellPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<ApparitionalDiskMagic>();
            item.UseSound = SoundID.DD2_BookStaffCast;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(3, 9);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;

        }

    }
}
