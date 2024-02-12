using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class StarshiftObliterationBreakerShoot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 10;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
        }
    }
}
