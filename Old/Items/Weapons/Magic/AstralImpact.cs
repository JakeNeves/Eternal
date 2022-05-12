using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class AstralImpact : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Impact Spikes from the sky downwards straight" +
                "\n'It's rainging... OH NO!'");
        }

        public override void SetDefaults()
        {
            item.damage = 1000;
            item.magic = true;
            item.mana = 18;
            item.width = 44;
            item.height = 46;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 2.75f;
            item.value = Item.sellPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.shootSpeed = 0f;
            item.shoot = ModContent.ProjectileType<AstralImpactProjectile>();
            item.UseSound = SoundID.DD2_BookStaffCast;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(12, 48);
            for (int j = 0; j < numberProjectiles; j++)
            {
                /*Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);*/
                Projectile.NewProjectile(position.X + Main.rand.Next(-850, 850), position.Y - 650, 0, speedY, type, damage, knockBack, player.whoAmI);
            }
            return false;

        }

    }
}
