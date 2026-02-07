using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class IncineratorProjectileShoot2 : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Weapons/Magic/IncineratorProjectile";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 5;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = false;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 50);

        public override void AI()
        {
            for (float i = 0; i < 5.0f; i++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
