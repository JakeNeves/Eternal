using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Radiant
{
    public class ExocosmicBombProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

            // DisplayName.SetDefault("Exocosmic Bomb");
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Projectile.DamageType = ModContent.GetInstance<DamageClasses.Radiant>();
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.timeLeft == 400)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ExocosmicBombCharge"), Projectile.position);
            }

            if (Projectile.timeLeft >= 200)
            {
                int maxdusts = 6;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 50;
                    float dustSpeed = 8;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(new Vector2(Projectile.Center.X, Projectile.Center.Y) + offset, DustID.DemonTorch, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }

            var entitySource = Projectile.GetSource_FromAI();
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ExocosmicBombDischarge"), Projectile.position);

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.PurpleTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            for (int i = 0; i < 32; i++)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(new Vector2(10f, 0.0f), (double)MathHelper.ToRadians((i * 30 + Main.rand.Next(30))), new Vector2()), ModContent.ProjectileType<ExocosmicBolt>(), Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }
        }
    }
}
