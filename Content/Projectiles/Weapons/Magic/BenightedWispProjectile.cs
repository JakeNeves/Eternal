using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class BenightedWispProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            Lighting.AddLight(Projectile.position, 1.27f, 0.22f, 0.76f);

            var entitySource = Projectile.GetSource_FromAI();

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int k = 0; k < 5; k++)
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);

            if (Main.netMode != NetmodeID.MultiplayerClient)
                SoundEngine.PlaySound(SoundID.Item100, Projectile.position);
        }
    }
}
