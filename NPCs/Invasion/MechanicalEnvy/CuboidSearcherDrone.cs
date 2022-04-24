using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Invasion.MechanicalEnvy
{
    public class CuboidSearcherDrone : ModNPC
    {
        int attackTimer;

        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 64;
            npc.knockBackResist = -1f;
            npc.height = 58;
            npc.lifeMax = 1500;
            npc.defense = 16;
            npc.damage = 20;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Stone);
        }

        #region settings
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        #endregion

        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.spriteDirection = npc.direction = npc.Center.X < player.Center.X ? -1 : 1;
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }

            float speed = 18f;
            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.10F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.15F;
                    }
                }
            }
            float num10 = speed / length;
            xDir = xDir * num10;
            yDir = yDir * num10;
            if (npc.velocity.X < xDir)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0 && xDir > 0)
                    npc.velocity.X = npc.velocity.X + acceleration;
            }
            else if (npc.velocity.X > xDir)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0 && xDir < 0)
                    npc.velocity.X = npc.velocity.X - acceleration;
            }
            if (npc.velocity.Y < yDir)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0 && yDir > 0)
                    npc.velocity.Y = npc.velocity.Y + acceleration;
            }
            else if (npc.velocity.Y > yDir)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0 && yDir < 0)
                    npc.velocity.Y = npc.velocity.Y - acceleration;
            }

            int tentacleType = ModContent.NPCType<CuboidTenticle>();
            int numTentacles = 2;
            if (!justSpawned)
            {
                if (Main.expertMode)
                {
                    for (int k = 0; k < numTentacles + 2; k++)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, tentacleType, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else if (EternalWorld.hellMode)
                {
                    for (int k = 0; k < numTentacles * 4; k++)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, tentacleType, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                    }
                }
                else
                {
                    for (int k = 0; k < numTentacles; k++)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, tentacleType, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                    }
                }
                justSpawned = true;
            }

            attackTimer++;
            if (attackTimer == 100 || attackTimer == 102 || attackTimer == 104 || attackTimer == 106 || attackTimer == 108 || attackTimer == 110)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                int amountOfProjectiles = Main.rand.Next(1, 3);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, npc.damage, 1, Main.myPlayer, 0, 0);
                }
            }
            else if (attackTimer == 112)
            {
                attackTimer = 0;
            }
        }
    }
}
