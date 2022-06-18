using Microsoft.Xna.Framework;
using Terraria;

namespace Eternal.Content.Projectiles.Weapons.Whips
{
    public class ExoSpinesnapperProjectile : Whip
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exo Spinesnapper");
        }
        public override void WhipDefaults()
        {
            originalColor = Color.White;
            whipRangeMultiplier = 1.5f;
            fallOff = 0.4f;
        }

        public override void ExtraWhipBehavior()
        {
            Lighting.AddLight(Projectile.position, 1.30f, 0.30f, 0.42f);
        }
    }
}
