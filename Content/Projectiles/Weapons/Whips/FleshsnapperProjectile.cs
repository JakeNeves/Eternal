using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Eternal.Content.Projectiles.Weapons.Whips
{
    public class FleshsnapperProjectile : Whip
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fleshsnapper");
        }

        public override void WhipDefaults()
        {
            originalColor = Color.White;
            whipRangeMultiplier = 1.5f;
            fallOff = 0.4f;
        }

        public override void ExtraWhipBehavior()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}
