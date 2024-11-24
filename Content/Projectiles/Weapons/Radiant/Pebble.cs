using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Radiant
{
    public class Pebble : ModProjectile
    {
        int shootTimer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = ModContent.GetInstance<DamageClasses.Radiant>();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            Projectile.Kill();
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation += Projectile.velocity.X * 0.1f;

            shootTimer++;

            var entitySource = Projectile.GetSource_FromAI();

            if (shootTimer >= 10)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<PebbleShoot>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                shootTimer = 0;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            for (int i = 0; i < 25; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 12.5f * i)) * 15;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Stone);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }
        }
    }
}
