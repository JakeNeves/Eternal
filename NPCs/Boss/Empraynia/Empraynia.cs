using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Eternal.NPCs.Boss.CosmicApparition;

namespace Eternal.NPCs.Boss.Empraynia
{
    [AutoloadBossHead]
    public class Empraynia : ModNPC
    {
        int timer;
        int frameNum;
        int phase;
        int attackNo;

        private Player player;

        bool expert = Main.expertMode;

        bool hands = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            Main.npcFrameCount[npc.type] = 4;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Texture2D shadowTexture = mod.GetTexture("NPCs/Boss/Empraynia/Empraynia_Shadow");
                spriteBatch.Draw(shadowTexture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 78000;
            npc.width = 148;
            npc.height = 173;
            npc.damage = 180;
            npc.defense = 64;
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.noTileCollide = true;
            music = MusicID.Boss4;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.HitSound = SoundID.NPCHit12;
            npc.DeathSound = SoundID.NPCDeath5;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 80000;
            npc.damage = 182;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 82000;
                npc.damage = 184;
            }
        }

        public override bool PreAI()
        {

            if(npc.life  < npc.lifeMax / 2)
            {
                phase = 1;
            }

            if (phase == 1)
            {
                frameNum = 3;
            }
            else
            {
                if (attackNo == 1)
                {
                    frameNum = 1;
                }
                else if (attackNo == 2)
                {
                    frameNum = 2;
                }
                else
                {
                    frameNum = 0;
                }
            }

            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                npc.velocity.Y = -100;
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
                        Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.Shadowflames, 28, 0, Main.myPlayer);
                    }
                }
                #endregion

            }
            return true;
        }

        public override void AI()
        {
            if (!hands && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                hands = true;
                NPC eHand = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width * 2)), (int)npc.position.Y + npc.height * 2, ModContent.NPCType<EmprayniaHand>(), 0, 0f, 0f, 0f, 0f, 255)];
                eHand.ai[0] = 1f;
                eHand.ai[1] = (float)npc.whoAmI;
                eHand.target = npc.target;
                eHand.netUpdate = true;
                eHand = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width * 2)), (int)npc.position.Y + npc.height * 2, ModContent.NPCType<EmprayniaHand>(), 0, 0f, 0f, 0f, 0f, 255)];
                eHand.ai[0] = -1f;
                eHand.ai[1] = (float)npc.whoAmI;
                eHand.target = npc.target;
                eHand.netUpdate = true;
            }

            if (npc.active)
            {
                if (!SkyManager.Instance["Eternal:Empraynia"].IsActive())
                {
                    SkyManager.Instance.Activate("Eternal:Empraynia");
                }
            }
            else
            {
                Main.NewText("The sky resumes back to it's tranquil state...", 220, 0, 210);
                SkyManager.Instance.Deactivate("Eternal:Empraynia");
            }

            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            timer++;
            if ((timer == 200 || timer == 400 && npc.life >= (npc.lifeMax / 2)))
            {
                //Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 73);

                //Main.PlaySound(SoundID.DD2_LightningBugZap, npc.position);
                Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                int amountOfProjectiles = Main.rand.Next(8, 11);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int damage = expert ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DD2DrakinShot, damage, 1, Main.myPlayer, 0, 0);
                }
            }
            else if ((timer == 600 || timer == 650 || timer == 700 || timer == 800 || timer == 850 || timer == 880))
            {
                attackNo = 2;
                //Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 73);
                Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    int damage = expert ? 15 : 19;
                    if (EternalWorld.hellMode)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 14f, direction.Y * 14f, ProjectileID.ShadowBeamHostile, damage, 1, Main.myPlayer, 0, 0);
                    }
                    else {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 14f, direction.Y * 14f, ProjectileID.DD2DarkMageBolt, damage, 1, Main.myPlayer, 0, 0);
                    }
                }
            }
            else if ((timer == 900 || timer == 950))
            {
                if (Main.rand.Next(50) == 0)
                {
                    attackNo = 1;
                    Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                }
                else
                {
                    attackNo = 1;
                    Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, -12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, -12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 12, ProjectileID.ShadowBeamHostile, 6, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (timer == 1000)
            {
                timer = 0;
                attackNo = 0;
            }
            else
            {
                attackNo = 0;
            }
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
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, -12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 12, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);

            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.DD2DarkMageBolt, 6, 0, Main.myPlayer, 0f, 0f);

            if (Main.expertMode)
            {
                //npc.DropBossBags();
            }
            else
            {
                
            }
            Main.NewText("The sky resumes back to it's tranquil state...", 220, 0, 210);
            SkyManager.Instance.Deactivate("Eternal:Empraynia");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Boss/Empraynia/Empraynia_Glow"));

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
