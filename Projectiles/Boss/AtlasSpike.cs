using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Boss
{
    public class AtlasSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XR-2006 Atlas-X9 Armor Penetrating Spike");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 22;
            projectile.height = 52;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.RedFracture>(), 4096);
        }

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            projectile.alpha -= 1;

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 60f)
                {
                    float velXmult = 0.90f;
                    float velYmult = 0.80f;
                    projectile.ai[1] = 60f;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath14, projectile.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }

    }
}
