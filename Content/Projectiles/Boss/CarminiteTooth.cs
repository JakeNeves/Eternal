using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class CarminiteTooth : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            target.AddBuff(BuffID.Bleeding, 480, false);

            Projectile.timeLeft = 0;
            Projectile.Kill();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        private const float maxTicks = 80f;
        private const int alphaReducation = 25;

        public override void AI()
        {
            Projectile.alpha += 1;

            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= alphaReducation;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= maxTicks)
                {
                    float velXmult = 0.20f;
                    float velYmult = 0.64f;
                    Projectile.ai[1] = maxTicks;
                    Projectile.velocity.X = Projectile.velocity.X * velXmult;
                    Projectile.velocity.Y = Projectile.velocity.Y + velYmult;
                }

                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.alpha = 255;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Bone, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            SoundEngine.PlaySound(SoundID.NPCHit2, Projectile.position);

            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
        }
    }
}
