using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class BOTAProjectileTrail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.alpha = 0;
            Projectile.scale = 1;
            Projectile.aiStyle = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            Projectile.alpha++;
            Projectile.scale -= 0.01f;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
        }
    }
}
