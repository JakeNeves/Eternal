using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Accessories
{
    public class GodheadCircle : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 100000;
        }

        public override void AI()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (!proj.active)
                    continue;

                if (proj.DamageType != DamageClass.Melee || proj.DamageType != DamageClass.Magic && !proj.friendly && !proj.active)
                {
                    Projectile.Kill();
                }

                Projectile.Center = proj.Center;
            }
        }
    }
}
