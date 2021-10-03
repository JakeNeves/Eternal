using Eternal.Items.Weapons.Throwing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class TheTrueCleaverSpread : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Cleaver");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 90;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 0.25f, projectile.oldVelocity.Y * 0.25f);
            }

            projectile.spriteDirection = projectile.direction;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);
            }
            Main.PlaySound(SoundID.NPCHit3, projectile.position);

        }
    }
}
