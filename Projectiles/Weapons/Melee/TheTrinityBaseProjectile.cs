using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class TheTrinityBaseProjectile : ModProjectile
    {
        public bool yoyoSpawn = false;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 575f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 25f;
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override void AI()
        {
            if (!yoyoSpawn && projectile.owner == Main.myPlayer)
            {
                int maxYoyos = 3;
                for (int i = 0; i < maxYoyos; i++)
                {
                    float radians = (360f / (float)maxYoyos) * i * (float)(Math.PI / 180);
                    Projectile yoyo = EternalGlobalProjectile.NewProjectileDirectSafe(projectile.Center, Vector2.Zero, ModContent.ProjectileType<TheTrinityProjectileThunder>(), projectile.damage, projectile.knockBack, projectile.owner, 5, radians);
                    yoyo.localAI[0] = projectile.whoAmI;
                }

                for (int i = 0; i < maxYoyos; i++)
                {
                    float radians = (360f / (float)maxYoyos) * i * (float)(Math.PI / 180);
                    Projectile yoyo = EternalGlobalProjectile.NewProjectileDirectSafe(projectile.Center, Vector2.Zero, ModContent.ProjectileType<TheTrinityProjectileFlame>(), projectile.damage, projectile.knockBack, projectile.owner, 5, radians);
                    yoyo.localAI[0] = projectile.whoAmI;
                }

                for (int i = 0; i < maxYoyos; i++)
                {
                    float radians = (360f / (float)maxYoyos) * i * (float)(Math.PI / 180);
                    Projectile yoyo = EternalGlobalProjectile.NewProjectileDirectSafe(projectile.Center, Vector2.Zero, ModContent.ProjectileType<TheTrinityProjectileIce>(), projectile.damage, projectile.knockBack, projectile.owner, 5, radians);
                    yoyo.localAI[0] = projectile.whoAmI;
                }

                yoyoSpawn = true;
            }


            if (Main.player[projectile.owner].HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.TheTrinity>())
            {
                projectile.damage = Main.player[projectile.owner].GetWeaponDamage(Main.player[projectile.owner].HeldItem);
                projectile.knockBack = Main.player[projectile.owner].GetWeaponKnockback(Main.player[projectile.owner].HeldItem, Main.player[projectile.owner].HeldItem.knockBack);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (projectile.Distance(targetHitbox.Center()) <= 70);
        }

    }
}
