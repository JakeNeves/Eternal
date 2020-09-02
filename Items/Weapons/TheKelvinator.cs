using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    public class TheKelvinator : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Two Arrows\n'Now you get to split the massive icicles!'");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 56;
            item.damage = 175;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 8f;
            item.shoot = ProjectileID.FrostArrow;
            item.rare = ItemRarityID.Yellow;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
