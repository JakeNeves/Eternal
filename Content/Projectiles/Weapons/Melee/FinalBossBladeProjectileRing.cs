﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class FinalBossBladeProjectileRing : ModProjectile
    {
        int shootTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Final Boss Blade");
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation += 0.15f;

            shootTimer++;

            var entitySource = Projectile.GetSource_FromAI();

            if (shootTimer > 20)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<FinalBossBladeProjectileRingPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                shootTimer = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            var entitySource = Projectile.GetSource_FromThis();

            for (int i = 0; i < 25; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.WhiteTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, -4, ModContent.ProjectileType<FinalBossBladeProjectileRingPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, -4, ModContent.ProjectileType<FinalBossBladeProjectileRingPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, 4, ModContent.ProjectileType<FinalBossBladeProjectileRingPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, 4, ModContent.ProjectileType<FinalBossBladeProjectileRingPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
        }
    }
}