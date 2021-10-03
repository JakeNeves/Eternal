using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class TheCleaverProjectile : ModProjectile
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cleaver");
		}

		public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 34;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
			projectile.tileCollide = true;
            projectile.penetrate = -1;
			projectile.melee = true;
        }

		public override void AI()
		{
			projectile.rotation += projectile.velocity.X * 0.1f;
			projectile.rotation += projectile.velocity.Y * 0.1f;

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
		}
    }
}
