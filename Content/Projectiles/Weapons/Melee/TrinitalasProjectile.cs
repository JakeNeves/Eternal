using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class TrinitalasProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 15f);

        int projTimer = 45;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 20f;
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (projTimer <= 0)
            {
                projTimer = 45;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.30f, new Vector2());
                        int index5 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, CircleDirc, ModContent.ProjectileType<TrinitalasShotProjectile>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<TrinitalasShotProjectile>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }
            }
            else
                projTimer--;
        }
    }
}
