using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class SanguinebeamFriendly : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Enemy/Sanguinebeam";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 50;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Sanguinebeam>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.Sanguinebeam>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
