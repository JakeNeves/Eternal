using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class ArkEnergyHostile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.alpha = 255;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            float dustScale = 1f;
            if (projectile.ai[0] == 0f)
                dustScale = 0.25f;
            else if (projectile.ai[0] == 1f)
                dustScale = 0.5f;
            else if (projectile.ai[0] == 2f)
                dustScale = 0.75f;

            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<ArkEnergy>(), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                if (Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                }

                dust.scale *= 1.5f;
                dust.velocity *= 1.2f;
                dust.scale *= dustScale;
            }
            projectile.ai[0] += 1f;
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
