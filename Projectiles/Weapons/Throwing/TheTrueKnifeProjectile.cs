using Eternal.Items.Weapons.Throwing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Throwing
{
    public class TheTrueKnifeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Knife");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 50;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }

            projectile.spriteDirection = projectile.direction;

            if (projectile.timeLeft == 0)
            {
                Main.NewText("The knife returns to you from a vast distance...", 50, 255, 130);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
                projectile.Kill();
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
            }
            Main.PlaySound(SoundID.Dig, projectile.position);

            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -8, 0, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 8, 0, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 8, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -8, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, -4, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, -4, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, 4, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, 4, ModContent.ProjectileType<TrueKnifeBolt>(), 6, 0, Main.myPlayer, 0f, 0f);
        }
    }
}
