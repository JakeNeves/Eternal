using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class IncineratorProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = false;
            Projectile.extraUpdates = 2;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 50);

        public override void AI()
        {
            for (float i = 0;  i < 5.0f; i++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(12f, 0f), ModContent.ProjectileType<IncineratorProjectileShoot>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<IncineratorProjectileShoot>(), Projectile.damage / 2, 0);
            }
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
