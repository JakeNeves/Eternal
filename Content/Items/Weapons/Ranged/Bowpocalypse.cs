using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class Bowpocalypse : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Shoots a random assortment of arrows");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 78;
            Item.damage = 1000;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 9.5f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2 + Main.rand.Next(2);
            float rotation = MathHelper.ToRadians(30);

            position += Vector2.Normalize(velocity) * 15f;
            
            for (int i = 0; i < numberProjectiles; i++)
            {

                if (Main.rand.NextBool(2))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.IchorArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(3))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.BeeArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(4))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.CursedArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(5))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.HellfireArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(6))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.MoonlordArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(7))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.ShadowFlameArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(8))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.JestersArrow, damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(9))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<SwiftShotStarbuster>(), damage, knockback, player.whoAmI);
                }
                if (Main.rand.NextBool(9))
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<StarbornArrowProjectile>(), damage, knockback, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
                }
            }

            return false;
        }
    }
}
