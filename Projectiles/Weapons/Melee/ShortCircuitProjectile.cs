using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Eternal.Items.Materials;
using Microsoft.Xna.Framework;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class ShortCircuitProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Short Circuit");
        }

        public override void SetDefaults()
        {
            projectile.width = 162;
            projectile.height = 162;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
            projectile.friendly = true;
        }

        public override void AI()
        {
            float rotationTime = 80f;
            float wtf = 2f;
            float qrtPi = -(float)Math.PI / 4f;

            float scaleFactor = 25f;

            Player player = Main.player[projectile.owner];

            Vector2 relPoint = player.RotatedRelativePoint(player.MountedCenter);

            if (!player.active || player.dead)
            {
                projectile.Kill();
                return;
            }

            Lighting.AddLight(player.Center, 1.90f, 0.22f, 0.22f);

            int sign = Math.Sign(projectile.velocity.X);

            projectile.velocity = new Vector2(sign, 0f);

            if (projectile.ai[0] == 0f)
            {
                projectile.rotation = new Vector2(sign, 0f - player.gravDir).ToRotation() + qrtPi + (float)Math.PI;
                if (projectile.velocity.X < 0f) {
                    projectile.rotation -= (float)Math.PI / 2f;
                }
            }

            projectile.ai[0] += 1f;
            projectile.rotation += (float)Math.PI * 2f * wtf / rotationTime * (float)sign;

            bool isDone = projectile.ai[0] == (rotationTime / 2f);

            if (projectile.ai[0] >= rotationTime || (isDone && !player.controlUseItem))
            {
                projectile.Kill();
                player.reuseDelay = 2;
            }
            else if (isDone)
            {
                Vector2 mouseWorld = Main.MouseWorld;
                int dir = (player.DirectionFrom(mouseWorld).X > 0f) ? 1 : -1;
                if ((float)dir != projectile.velocity.X)
                {
                    player.ChangeDir(dir);
                    projectile.velocity = new Vector2(dir, 0f);
                    projectile.netUpdate = true;
                    projectile.rotation -= (float)Math.PI;
                }
            }

            float rotVal = projectile.rotation - (float)Math.PI / 4f * (float)sign;
            Vector2 posVec = (rotVal + (sign == -1 ? (float)Math.PI : 0f)).ToRotationVector2() * (projectile.ai[0] / rotationTime) * scaleFactor;

            projectile.position = relPoint - projectile.Size / 2f;
            projectile.position += posVec;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;

            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(projectile.rotation);

        }
    }
}
