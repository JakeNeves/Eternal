using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class BOTAProjectileAOE : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
            Projectile.alpha = 0;
            Projectile.scale = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            Projectile.alpha += 5;
            Projectile.scale -= 0.05f;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAAOEHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                    MaxInstances = 0,
                }, Projectile.position);
            }

            Projectile.Kill();
        }
    }
}
