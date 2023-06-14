using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class NyanarangProjectile : ModProjectile
    {
        int pierceTimer = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nyanarang");
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            pierceTimer++;

            var entitySource = Projectile.GetSource_FromAI();

            if (pierceTimer >= 20)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ProjectileID.Meowmere, Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                pierceTimer = 0;
            }
        }
    }
}
