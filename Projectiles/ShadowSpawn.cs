using Eternal.Dusts;
using Eternal.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles
{
    public class ShadowSpawn : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 32;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.alpha = 0;
            projectile.tileCollide = false;
        }

        private const float maxTicks = 80f;
        private const int alphaReducation = 25;

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 54, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }

            if (projectile.alpha > 0)
            {
                projectile.alpha -= alphaReducation;
            }

            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= maxTicks)
                {
                    float velXmult = 0f;
                    float velYmult = -0.10f;
                    projectile.ai[1] = maxTicks;
                    projectile.velocity.X = projectile.velocity.X * velXmult;
                    projectile.velocity.Y = projectile.velocity.Y + velYmult;
                }

            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Roar, (int)projectile.position.X, (int)projectile.position.Y, 0);
            NPC.NewNPC((int)projectile.Center.X - 20, (int)projectile.Center.Y, ModContent.NPCType<ShadowMonster>());
            Main.NewText("A mysterious shadow monster is after you!", 175, 75, 255);
        }

    }
}
