using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class StarshiftObliterationBreakerProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 360f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 30f;
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
            int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ModContent.ProjectileType<StarshiftObliterationBreakerShoot>(), Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<StarshiftObliterationBreakerShoot>(), Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            Main.projectile[index5].timeLeft = 10;
            Main.projectile[index6].timeLeft = 10;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (Projectile.Distance(targetHitbox.Center()) <= 70);
        }
    }
}
