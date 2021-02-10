using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class StarcrescentProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starcrescent Shard");
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
        }

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            projectile.rotation += projectile.velocity.X * 0.1f;

            projectile.alpha += 1;
        }

        public override void Kill(int timeLeft)
        {
            projectile.alpha = 255;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Electric, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.NPCDeath44, projectile.position);
        }
    }
}
