using Microsoft.Xna.Framework;
using Eternal.Content.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.Content.Projectiles.Boss
{
    public class SpiritBomb2 : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Boss/SpiritBomb";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
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
