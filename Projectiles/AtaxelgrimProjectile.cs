using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles
{
    class AtaxelgrimProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ataxelgrim");
        }

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 46;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.hide = true;
            projectile.aiStyle = 1;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.ThrowingKnife;
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 50f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
                projectile.velocity.X = projectile.velocity.X * 0.99f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            {
                projectile.Kill();

                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            }
            return false;
        }

    }
}
