using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class TheTrueCleaverProjectile : ModProjectile
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cleaver");
		}

		public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 42;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.melee = true;
			projectile.timeLeft = 600;
        }

		public override void AI()
		{
			projectile.rotation += projectile.velocity.X * 0.1f;

			if (projectile.timeLeft <= 590)
				projectile.alpha--;

			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f)
			{
				projectile.ai[0] = 10f;
				if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
				{
					projectile.velocity.X = projectile.velocity.X * 0.97f;
					{
						projectile.velocity.X = projectile.velocity.X * 0.99f;
					}
					if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
					{
						projectile.velocity.X = 0f;
						projectile.netUpdate = true;
					}
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}
			projectile.rotation += projectile.velocity.X * 0.1f;
			return;
		}

        public override void Kill(int timeLeft)
        {
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 54, projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
			}
			Main.PlaySound(SoundID.Dig, projectile.position);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8, 0, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8, 0, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 8, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -8, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, -4, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, -4, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, 4, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, 4, ModContent.ProjectileType<TheTrueCleaverSpread>(), 6, 0, Main.myPlayer, 0f, 0f);
		}
    }
}
