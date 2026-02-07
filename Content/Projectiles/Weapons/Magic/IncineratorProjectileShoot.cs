using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class IncineratorProjectileShoot : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Weapons/Magic/IncineratorProjectile";

        ref float Timer => ref Projectile.ai[1];

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = false;
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 50);

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            Timer++;

            if (Timer >= 8f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.position);

                if (Projectile.velocity.X != 0)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -4f), ModContent.ProjectileType<IncineratorProjectileShoot2>(), Projectile.damage, 0);
                }

                Timer = 0;
            }

            for (float i = 0; i < 5.0f; i++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath3, Projectile.position);

            for (int k = 0; k < 5; k++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
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
