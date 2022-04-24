using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Accessories
{
    public class IceWheelProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 202;
            projectile.height = 202;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 500);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.Center = player.Center;

            projectile.rotation += 0.1f;

            if (!EternalPlayer.frostKingsCore || !player.active || player.dead)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.timeLeft <= 4)
                {
                    projectile.timeLeft = 600;
                }
            }

            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Frost, projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
