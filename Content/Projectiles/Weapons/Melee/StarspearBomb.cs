using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class StarspearBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 250;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(Projectile.position, 0.24f, 0.22f, 1.90f);

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            var entitySource = Projectile.GetSource_FromThis();

            for (int i = 0; i < 15; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.PurpleTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            if (!Main.dedServ) {
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, -4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, -4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, -4, 4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, 4, 4, ModContent.ProjectileType<StarspearBombPierce>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
            }
        }
    }
}
