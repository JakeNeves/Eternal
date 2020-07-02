using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;

namespace Eternal.Projectiles
{
    class BloodtoothProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodtooth");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 3;

        }

		public override void AI()
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                projectile.alpha = 255;
                projectile.position = projectile.Center;
                projectile.width = 20;
                projectile.height = 24;
                projectile.Center = projectile.position;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(projectile.ai[1] != 0)
            {
                return true;
            }

            if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                projectile.velocity.X = oldVelocity.X * -0.5f;
            }
            if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                projectile.velocity.Y = oldVelocity.Y * -0.5f;
            }
            return false;

        }

    }
}
