﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class StarspearBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 250;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, 0.24f, 0.22f, 1.90f);

            Projectile.rotation += 0.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            var entitySource = Projectile.GetSource_FromThis();

            for (int i = 0; i < 25; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, -4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, -4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, 4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, 4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
        }
    }
}
