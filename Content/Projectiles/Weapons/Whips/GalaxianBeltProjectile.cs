using Microsoft.Xna.Framework;
using Terraria;

namespace Eternal.Content.Projectiles.Weapons.Whips
{
    public class GalaxianBeltProjectile : Whip
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galaxian Belt");
        }
        public override void WhipDefaults()
        {
            originalColor = Color.White;
            whipRangeMultiplier = 1.5f;
            fallOff = 0.4f;
        }

        public override void ExtraWhipBehavior()
        {
            Lighting.AddLight(Projectile.position, 0.75f, 0f, 0.75f);
        }
    }
}
