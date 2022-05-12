using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class FinalBossBladeProjectile2 : ModProjectile
    {
		int timer = 0;

		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Final Boss Blade (Homing Chakram Swarm)");
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 4;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 2;
		}

        public override void AI()
        {
			float maxDetectRadius = 50f;
			float projSpeed = 12f;

			timer++;

			projectile.rotation += projectile.velocity.X * 0.1f;

			if (timer >= 25)
			{
				NPC closestNPC = FindClosestNPC(maxDetectRadius);
				if (closestNPC == null)
					return;

				projectile.velocity = (closestNPC.Center - projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
			}
        }

		public NPC FindClosestNPC(float maxDetectDistance)
        {
			NPC closestNPC = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
            {
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy())
                {
					float sqrDistanceToTarget = Vector2.Distance(target.Center, projectile.Center);

					if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
                    }
                }
            }

			return closestNPC;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				projectile.velocity *= 0.75f;
				Main.PlaySound(SoundID.NPCHit3, projectile.position);
			}
			return false;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			projectile.Kill();
        }

        public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Electric, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.NPCHit3, projectile.position);
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			int size = 30;
			hitbox.X -= size;
			hitbox.Y -= size;
			hitbox.Width += size * 2;
			hitbox.Height += size * 2;
		}

	}
}
