using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class CosmicFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 260;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }

            Projectile.alpha++;

            Lighting.AddLight(Projectile.position, 1.98f, 0.49f, 2.47f);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }

            if (Projectile.localAI[0] == 0f)
            {
                int whateverThisIntIs = 0;
                while ((float)whateverThisIntIs < 16f)
                {
                    Vector2 vector2 = Vector2.UnitX * 0f;
                    vector2 += -Vector2.UnitY.RotatedBy((double)((float)whateverThisIntIs * (6.28318548f / 16f)), default(Vector2)) * new Vector2(1f, 4f);
                    vector2 = vector2.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                    whateverThisIntIs++;
                }
                Projectile.localAI[0] = 1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.alpha = 255;
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

    }
}
