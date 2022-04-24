using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Ranged
{
    public class ScorchthrowerFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamethrower Projectile");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 6;
            projectile.timeLeft = 100;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            if (projectile.wet)
            {
                projectile.Kill();
            }

            float dustScale = 1f;
            if (projectile.ai[0] == 0f)
                dustScale = 0.25f;
            else if (projectile.ai[0] == 1f)
                dustScale = 0.5f;
            else if (projectile.ai[0] == 2f)
                dustScale = 0.75f;

            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.BlueTorch, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 480);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 480, false);
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
