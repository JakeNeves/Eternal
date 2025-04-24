using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class ValtoricKnivesProjectile : ModProjectile
    {
        static int knifeTimeMax = 360;
        int knifeTime = knifeTimeMax;

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 360;
            Projectile.alpha = 0;
            Projectile.extraUpdates = 5;
        }

        public override void AI()
        {
            knifeTime--;

            if (knifeTime < knifeTimeMax / 2)
                Projectile.rotation += 15f;
            else
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (knifeTime < knifeTimeMax / 3)
                Projectile.alpha += 25;

            if (Projectile.alpha >= 255)
                Projectile.Kill();
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
            target.AddBuff(BuffID.BrokenArmor, 360);

            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.BrokenArmor, 360);

            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            Projectile.Kill();
        }
    }
}
