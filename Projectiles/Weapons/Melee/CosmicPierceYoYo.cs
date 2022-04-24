using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class CosmicPierceYoYo : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.timeLeft = 0;
            projectile.Kill();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            float maxDetectRadius = 200f;
            float projSpeed = 6f;

            timer++;

            if (timer >= 75)
            {
                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null)
                    return;

                projectile.velocity = (closestNPC.Center - projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            }
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.Distance(target.Center, projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void Kill(int timeLeft)
        {
            projectile.alpha = 255;

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustType<Starmetal>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.DD2_CrystalCartImpact, projectile.position);

            Vector2 usePos = projectile.position;
            Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
        }
    }
}
