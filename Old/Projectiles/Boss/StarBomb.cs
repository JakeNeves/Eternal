using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    //Cosmic Emperor Superboss Projectile
    public class StarBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 10;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.timeLeft = 200;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.light = 0.75f;
        }

        public override void AI()
        {
            //Animation
            if (++projectile.frameCounter >= 11)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 10)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.timeLeft == 150)
            {
                Main.PlaySound(SoundID.Item4, projectile.position);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_SkyDragonsFurySwing, projectile.position);

            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -12, 0, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 12, 0, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 12, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, -12, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -10, -10, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 10, -10, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -10, 10, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 10, 10, ModContent.ProjectileType<EmperorBlade>(), 6, 0, Main.myPlayer, 0f, 0f);
        }
    }
}
