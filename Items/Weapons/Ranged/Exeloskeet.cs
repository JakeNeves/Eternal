using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class Exeloskeet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FF8949:Terry Item]\nFires fourteen bullets at once\nIt's a little op for an endgame weapon but it does the job...\n'Wait, this isn't right");
        }

        public override void SetDefaults()
        {
            item.width = 72;
            item.height = 40;
            item.damage = 777;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.NPCHit41;
            item.autoReuse = true;
            item.shootSpeed = 7f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(777, 77, 7);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 7;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
