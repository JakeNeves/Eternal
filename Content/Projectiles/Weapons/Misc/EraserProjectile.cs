using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Misc
{
    public class EraserProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.active = false;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
    }
}
