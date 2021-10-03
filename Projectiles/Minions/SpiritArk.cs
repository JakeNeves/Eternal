using Eternal.Buffs.Minions;
using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Minions
{
    public class SpiritArk : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Ark");

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;

            Main.projPet[projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 72;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            //projectile.light = 0.5f;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<SpiritArkWisp>(), projectile.oldVelocity.X * 1f, projectile.oldVelocity.Y * 1f);

            Lighting.AddLight(projectile.position, 0.87f, 2.55f, 1.62f);

            Player player = Main.player[projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<SpiritArkBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<SpiritArkBuff>()))
            {
                projectile.timeLeft = 2;
            }

            float distanceFromTarget = 600f;
            Vector2 targetCenter = projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, projectile.Center);
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }
            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, projectile.Center);
                        bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
                        bool closeThroughWall = between < 100f;
                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f;

            float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX;

            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }

            float speed = 12f;
            float inertia = 20f;

            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    Vector2 direction = targetCenter - projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
                }
            }
            else
            {
                if (distanceToIdlePosition > 600f)
                {
                    speed = 60f;
                    inertia = 60f;
                }
                else
                {
                    speed = 16f;
                    inertia = 80f;
                }
                if (distanceToIdlePosition > 20f)
                {
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (projectile.velocity == Vector2.Zero)
                {
                    projectile.velocity.X = -0.20f;
                    projectile.velocity.Y = -0.05f;
                }
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(mod.GetTexture("Projectiles/Minions/SpiritArk_Shadow"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

    }
}
