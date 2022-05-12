using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class StarspearBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparspear Bomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 250;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.24f, 0.22f, 1.90f);

            projectile.rotation *= 0.5f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath14, projectile.position);

            for (int i = 0; i < 25; i++)
            {
                Vector2 position = projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(projectile.position, DustID.PurpleTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, -4, ModContent.ProjectileType<StarspearBombPierce>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, -4, ModContent.ProjectileType<StarspearBombPierce>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4, 4, ModContent.ProjectileType<StarspearBombPierce>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4, 4, ModContent.ProjectileType<StarspearBombPierce>(), projectile.damage, 0, Main.myPlayer, 0f, 0f);
        }
    }
}
