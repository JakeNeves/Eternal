using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Accessories
{
    public class GasCloudFriendly : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 28;
            Projectile.alpha = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 200)
                Projectile.alpha += 10;

            if (Projectile.alpha >= 255)
                Projectile.Kill();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 1 * 60 * 30, false);
        }
    }
}
