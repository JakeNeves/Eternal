using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Invasion.MechanicalEnvy
{
    public class MegaCuboidWardingDrone : ModNPC
    {
        int timer;
        int frameNum;
        int phase;
        int attackNo;

        private Player player;

        bool expert = Main.expertMode;
        bool phase2Init = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 12000;
            npc.width = 128;
            npc.height = 114;
            npc.damage = 100;
            npc.defense = 64;
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.noTileCollide = true;
            music = MusicID.MartianMadness;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 24000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 36000;
            }
        }

        public override bool PreAI()
        {
            if (npc.life < npc.lifeMax / 2)
            {
                phase = 1;
                if (!phase2Init)
                {
                    Main.PlaySound(SoundID.NPCDeath14, (int)npc.position.X, (int)npc.position.Y);
                    CombatText.NewText(npc.Hitbox, Color.Red, "DAMAGE IS CRITICAL!", dramatic: true);
                    for (int i = 0; i < 25; i++)
                    {
                        Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                        Dust dust = Dust.NewDustPerfect(npc.position, DustID.Smoke);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Normalize(position - npc.Center) * 4;
                        dust.noLight = false;
                        dust.fadeIn = 1f;
                    }
                    phase2Init = true;
                }
            }

            if (phase == 1)
            {
                frameNum = 1;
            }
            else
            {
                frameNum = 0;
            }

            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                npc.velocity.Y = -100;
                if (npc.timeLeft > 30)
                    npc.timeLeft = 30;
                npc.velocity.Y -= 1f;
            }
            if (npc.ai[0] == 0)
            {
                #region Flying Movement
                float speed;
                if (EternalWorld.hellMode)
                {
                    speed = 10f;
                }
                else
                {
                    speed = 8f;
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
                #endregion

                #region close up attack
                float currentXDist = Math.Abs(npc.Center.X - player.Center.X);
                if (npc.Center.X < player.Center.X && npc.ai[2] < 0)
                    npc.ai[2] = 0;
                if (npc.Center.X > player.Center.X && npc.ai[2] > 0)
                    npc.ai[2] = 0;

                float yDist = player.position.Y - (npc.position.Y + npc.height);
                if (yDist < 0)
                    npc.velocity.Y = npc.velocity.Y - 0.2F;
                if (yDist > 150)
                    npc.velocity.Y = npc.velocity.Y + 0.2F;
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -6, 6);
                if (EternalWorld.hellMode)
                {
                    npc.rotation = npc.velocity.X * 0.06f;
                }
                else
                {
                    npc.rotation = npc.velocity.X * 0.03f;
                }

                if ((currentXDist < 500 || EternalWorld.hellMode) && npc.position.Y < player.position.Y)
                {
                    ++npc.ai[3];
                    int cooldown = 15;
                    if (npc.life < npc.lifeMax * 0.75)
                        cooldown = 154;
                    if (npc.life < npc.lifeMax * 0.5)
                        cooldown = 13;
                    if (npc.life < npc.lifeMax * 0.25)
                        cooldown = 12;
                    cooldown++;
                    if (npc.ai[3] > cooldown)
                        npc.ai[3] = -cooldown;

                    if (npc.ai[3] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 position = npc.Center;
                        position.X += npc.velocity.X * 7;

                        float speedX = player.Center.X - npc.Center.X;
                        float speedY = player.Center.Y - npc.Center.Y;
                        float num12 = speed / length;
                        speedX = speedX * num12;
                        speedY = speedY * num12;
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.DeathLaser, 28, 0, Main.myPlayer);
                    }
                }
                #endregion

            }
            return true;
        }

        public override void AI()
        {
            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
        }

        public override void FindFrame(int frameHeight)
        {
            //npc.frameCounter += 0.24f;
            //npc.frameCounter %= Main.npcFrameCount[npc.type];
            //int frame = (int)npc.frameCounter;
            npc.frame.Y = frameNum * frameHeight;
        }

        public override void NPCLoot()
        {
            player = Main.player[npc.target];
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        private bool AliveCheck(Player player)
        {
            if (player.dead)
            {
                if (npc.timeLeft > 30)
                    npc.timeLeft = 30;
                npc.velocity.Y -= 1f;
                return false;
            }
            return true;
        }

    }
}
