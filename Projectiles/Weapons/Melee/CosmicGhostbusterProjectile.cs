using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eternal.Projectiles.Weapons.Melee
{
    public class CosmicGhostbusterProjectile : ModProjectile
    {
        int timer;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 450f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 20f;
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1f;
        }

        public override void AI()
        {

            timer++;
            if (timer == 100)
            {
                Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, projectile.Center);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -12, 0, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 12, 0, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 12, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -12, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6, -6, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6, -6, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6, 6, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6, 6, ModContent.ProjectileType<CosmicPierceYoYo>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
            }
            else if(timer >= 120)
            {
                timer = 0;
            }
        }
    }
}
