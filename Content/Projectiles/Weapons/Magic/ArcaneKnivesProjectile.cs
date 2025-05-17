using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class ArcaneKnivesProjectile : ModProjectile
    {
        static int knifeTimeMax = 400;
        int knifeTime = knifeTimeMax;

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.alpha = 0;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            knifeTime--;

            if (knifeTime < knifeTimeMax / 2)
            {
                if (Main.rand.NextBool(2))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<MausoleumTorch>());
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = Main.rand.NextFloat(0.8f, 1f);
                    }
                }
            }

            if (knifeTime < knifeTimeMax / 3)
                Projectile.alpha += 25;

            if (Projectile.alpha >= 255)
                Projectile.Kill();

            if (Projectile.timeLeft < 300)
            {
                float maxDetectRadius = 100f;
                float projSpeed = 6f;

                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null)
                    return;

                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
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
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 36;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.manaRegenBonus += damageDone;
            player.manaRegen = 12;

            target.AddBuff(BuffID.ShadowFlame, 480);

            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            player.manaRegen = 12;

            target.AddBuff(BuffID.ShadowFlame, 480);

            Projectile.Kill();
        }
    }
}
