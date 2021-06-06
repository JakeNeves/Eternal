using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    //Cosmic Emperor Superboss Projectile
    public class CosmicRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.timeLeft = 100;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            //Animation
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.timeLeft == 50)
            {
                Main.PlaySound(SoundID.Item4, projectile.position);
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, projectile.position);

            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, -8, 0, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 8, 0, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 0, 8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 0, -8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, -8, -8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 8, -8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, -8, 8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.position.X + 40, projectile.position.Y + 40, 8, 8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
        }
    }
}
