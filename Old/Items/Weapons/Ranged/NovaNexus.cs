﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class NovaNexus : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'For when things get out of hand and start to cause chaos...'");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 26;
            item.damage = 110;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/NovaNexus");
            item.autoReuse = true;
            item.shootSpeed = 5f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
            item.knockBack = 2f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 3 + Main.rand.Next(3);
            float rotation = MathHelper.ToRadians(30);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            for (int i = 0; i < numberProjectiles; i++)
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}