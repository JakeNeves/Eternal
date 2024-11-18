using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class ArkiumDiskProjectileAOE : ModProjectile
    {
        float projScale = 1f;

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
        }

        public override void AI()
        {
            Projectile.rotation += 0.15f;
            Projectile.alpha++;
            Projectile.scale += 0.05f;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
        }
    }
}
