using Eternal.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class TheImperiousCohort : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Two Cohort Daggers\n<right> for True Melee");
        }

        public override void SetDefaults()
        {
            item.width = 76;
            item.height = 76;
            item.damage = 2600;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.useAnimation = 22;
            item.useTime = 22;
            item.shoot = ProjectileType<CohortDagger>();
            item.shootSpeed = 18.2f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
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

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.shoot = ProjectileID.None;
                item.shootSpeed = 0f;
            }
            else
            {
                item.shoot = ProjectileType<CohortDagger>();
                item.shootSpeed = 18.2f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            return false;
            
        }

    }
}
