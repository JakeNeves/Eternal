using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Armor
{
    public class NaquadahSpikeBombAOE : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.alpha = 0;
            Projectile.scale = 1;
        }

        public override void AI()
        {
            Projectile.scale += 0.3f;
            Projectile.alpha += 15;
            Projectile.scale += 0.15f;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
        }
    }
}
