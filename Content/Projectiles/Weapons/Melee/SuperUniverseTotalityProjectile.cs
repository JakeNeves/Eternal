using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class SuperUniverseTotalityProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        int Timer;
        int Timer2;

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
            Timer++;
            Timer2++;

            var entitySource = Projectile.GetSource_FromAI();

            if (!Main.dedServ)
            {
                if (Timer == 5)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ModContent.ProjectileType<SuperUniverseTotalityShoot>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<SuperUniverseTotalityShoot>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].timeLeft = 10;
                    Main.projectile[index6].timeLeft = 10;

                    Timer = 0;
                }

                if (Timer2 == 25)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<SuperUniverseTotalityShoot>(), Projectile.damage / 2, 0, Main.myPlayer, 0f, 0f);

                    Timer2 = 0;
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (Projectile.Distance(targetHitbox.Center()) <= 70);
        }
    }
}
