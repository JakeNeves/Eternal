using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class TheBigOneProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 164;
            Projectile.height = 162;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            --Projectile.penetrate;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
                if ((double)Math.Abs(Projectile.velocity.X - oldVelocity.X) > 1.4012984643248171E-45)
                    Projectile.velocity.X = -oldVelocity.X;
                if ((double)Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > 1.4012984643248171E-45)
                    Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }
    }
}
