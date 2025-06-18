using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class ArkiumDiskProjectileAOE : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 250;
            Projectile.alpha = 0;
            Projectile.scale = 1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Projectile.rotation += 0.15f;
            Projectile.alpha++;
            Projectile.scale += (0.05f * Projectile.scale) / 4;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
        }
    }
}
