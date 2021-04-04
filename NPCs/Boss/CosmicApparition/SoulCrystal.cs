using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Eternal.NPCs.Boss.CosmicApparition
{
    public class SoulCrystal : ModNPC
    {

        int phase = 0;
        int projectileShoot;
        int teleportTimer;
        int projectileTimer;

        bool canTeleport = true;

        const float Speed = 12f;
        const float Acceleration = 0.4f;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            npc.width = 35;
            npc.height = 69;
            npc.lifeMax = 12800;
            npc.defense = 18;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath44;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TrappedSoul");
            npc.damage = 200;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = -1f;
        }

        public override void NPCLoot()
        {
            Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 2);
            Main.NewText("Shrieks echo as the soul breaks free...", 175, 75, 255);
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicApparition>());
        }

        public override void AI()
        {
            if (npc.life < npc.lifeMax / 2)
            {
                phase = 1;
            }

            switch (phase)
            {
                case 0:
                    #region Flying Movement P1
                    if (phase == 0)
                    {
                        float speed;
                        if (EternalWorld.hellMode)
                        {
                            speed = 12f;
                        }
                        else
                        {
                            speed = 10f;
                        }
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
                                acceleration += 0.05F;
                                if (length > 800)
                                {
                                    ++speed;
                                    acceleration += 0.05F;
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
                    }
                    #endregion
                    break;
                case 1:
                    #region Flying Movement P2
                    Player player = Main.player[npc.target];
                    npc.TargetClosest(true);
                    npc.direction = npc.spriteDirection = npc.Center.X < player.Center.X ? -1 : 1;
                    Vector2 targetPosition = Main.player[npc.target].position;
                    Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
                    npc.netAlways = true;
                    if (canTeleport)
                    {
                        teleportTimer++;
                    }
                    if (teleportTimer >= 200)
                    {
                        Main.PlaySound(SoundID.Item8, Main.myPlayer);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Shadowflame, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                        }
                        npc.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                        npc.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);
                        teleportTimer = 0;
                    }
                    if (projectileTimer >= 180)
                    {
                        Vector2 shootPos = npc.Center;
                        float accuracy = 5f * (npc.life / npc.lifeMax);
                        Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                        shootVel.Normalize();
                        shootVel *= 14.5f;
                        projectileShoot = Main.rand.Next(0, 1);
                        for (int i = 0; i < (Main.expertMode ? 2 : 1); i++)
                        {
                            if (projectileShoot == 0)
                            {
                                Projectile.NewProjectile(shootPos.X + -100 * npc.direction + Main.rand.Next(-20, 20), shootPos.Y - Main.rand.Next(-20, 20), shootVel.X, shootVel.Y, ProjectileID.ShadowBeamHostile, npc.damage / 3, 5f);
                            }
                            if (projectileShoot == 1)
                            {
                                Projectile.NewProjectile(shootPos.X + -100 * npc.direction + Main.rand.Next(-20, 20), shootPos.Y - Main.rand.Next(-20, 20), shootVel.X, shootVel.Y, ProjectileID.Shadowflames, npc.damage / 2, 5f);
                            }
                        }
                        projectileTimer = 0;
                    }
                    if (!player.active || player.dead)
                    {
                        canTeleport = false;
                        npc.TargetClosest(false);
                        npc.direction = 1;
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                        if (npc.timeLeft > 5)
                        {
                            npc.timeLeft = 5;
                            return;
                        }
                    }
                    #endregion
                    break;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
