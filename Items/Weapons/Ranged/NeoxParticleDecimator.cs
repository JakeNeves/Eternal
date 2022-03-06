using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class NeoxParticleDecimator : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("N30X Particle Decimator");
        }

        public override void SetDefaults()
        {
            item.width = 52;
            item.height = 28;
            item.damage = 2100;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.DD2_BetsyFireballImpact;
            item.autoReuse = true;
            item.shootSpeed = 12f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
            item.knockBack = 4.5f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 2 + Main.rand.Next(6);
            float rotation = MathHelper.ToRadians(15);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 24f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
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
