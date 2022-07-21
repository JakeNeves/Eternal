using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Empraynia
{
    [AutoloadBossHead]
    public class Empraynia : ModNPC
    {
        int timer;
        int phase;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 78000;
            NPC.width = 114;
            NPC.height = 184;
            NPC.damage = 180;
            NPC.defense = 64;
            NPC.knockBackResist = -1f;
            NPC.boss = true;
            NPC.noTileCollide = true;
            Music = MusicID.Boss4;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.HitSound = SoundID.NPCHit12;
            NPC.DeathSound = SoundID.NPCDeath5;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life < 0)
            {
                var entitySource = NPC.GetSource_Death();

                int gore1 = Mod.Find<ModGore>("EmprayniaHead").Type;
                int gore2 = Mod.Find<ModGore>("EmprayniaBody").Type;
                int gore3 = Mod.Find<ModGore>("EmprayniaArm").Type;
                int gore4 = Mod.Find<ModGore>("EmprayniaWing").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore4);
                }
            }

            for (int k = 0; k < damage / NPC.lifeMax * 50; k++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 82000;
                NPC.damage = 184;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 84000;
                NPC.damage = 186;
            }
            else
            {
                NPC.lifeMax = 80000;
                NPC.damage = 182;
            }
        }

        public override bool PreAI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = -100;
            }
            NPC.rotation = NPC.velocity.X * 0.04f;
            if (NPC.ai[0] == 0)
            {
                #region Flying Movement
                float speed = 40f;
                float acceleration = 0.15f;
                Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
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
                if (NPC.velocity.X < xDir)
                {
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                    if (NPC.velocity.X < 0 && xDir > 0)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                }
                else if (NPC.velocity.X > xDir)
                {
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.X > 0 && xDir < 0)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                if (NPC.velocity.Y < yDir)
                {
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    if (NPC.velocity.Y < 0 && yDir > 0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                }
                else if (NPC.velocity.Y > yDir)
                {
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (NPC.velocity.Y > 0 && yDir < 0)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                }
                #endregion
            }
            return true;
        }

        public override void AI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            timer++;
            if ((timer == 200 || timer == 400 && NPC.life >= (NPC.lifeMax / 2)))
            {
            }
            else if ((timer == 600 || timer == 650 || timer == 700 || timer == 800 || timer == 850 || timer == 880))
            {
            }
            else if ((timer == 900 || timer == 950))
            {
            }
            if (timer == 1000)
            {
                timer = 0;
            }

            if (!player.active || player.dead)
            {
                if (NPC.timeLeft > 30)
                    NPC.timeLeft = 30;
                NPC.velocity.Y -= 1f;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            //tf does this supposed to mean
            int num159 = 1;
            float num160 = 0f;
            int num161 = num159;
            SpriteEffects spriteEffects = SpriteEffects.None;
            Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
            Texture2D texture2D4 = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/Empraynia/Empraynia_Wings").Value;
            int num1561 = texture2D4.Height / Main.npcFrameCount[NPC.type];
            int y31 = num1561 * (int)NPC.frameCounter;
            Microsoft.Xna.Framework.Rectangle rectangle2 = new Microsoft.Xna.Framework.Rectangle(0, y31, texture2D4.Width, num1561);
            Vector2 origin3 = rectangle2.Size() / 2f;
            SpriteEffects effects = spriteEffects;
            if (NPC.spriteDirection > 0)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            float num165 = NPC.rotation;
            Microsoft.Xna.Framework.Color color29 = NPC.GetAlpha(color25);
            Main.spriteBatch.Draw(texture2D4, NPC.position + NPC.Size / 2f - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle2), color29, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin3, NPC.scale, effects, 0f);

            return true;
        }

    }
}
