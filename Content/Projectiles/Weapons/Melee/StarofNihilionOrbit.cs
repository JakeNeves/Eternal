using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class StarofNihilionOrbit : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star of Nihilion");
        }

        public override void SetDefaults()
        {
            Projectile.height = 14;
            Projectile.width = 14;

            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;

            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {

            Projectile proj = Main.projectile[(int)Projectile.localAI[0]];

            Projectile.timeLeft++;
            Projectile.rotation += 0.1f;

            if (!proj.active || proj.type != ModContent.ProjectileType<StarofNihilionProjectile>() || proj.owner != Projectile.owner)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.owner == Main.myPlayer)
            {
                //rotating mf
                float distanceFromPlayer = 30;

                Projectile.position = proj.Center + new Vector2(distanceFromPlayer, 0f).RotatedBy(Projectile.ai[1]);
                Projectile.position.X -= Projectile.width / 2;
                Projectile.position.Y -= Projectile.height / 2;

                float rotation = (float)Math.PI / 60;
                Projectile.ai[1] += rotation;
                if (Projectile.ai[1] > (float)Math.PI)
                {
                    Projectile.ai[1] -= 2f * (float)Math.PI;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.damage = proj.damage;
            Projectile.knockBack = proj.knockBack;
        }
    }
}
