using Eternal.Common.Systems;
using Eternal.Content.BossBars;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    //[AutoloadBossHead]
    public class SpiritofIncinerius : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Incinerius");

            Main.npcFrameCount[NPC.type] = 4;
        }

        #region Fundementals
        const float Speed = 10f;
        const float Acceleration = 4f;

        int Timer;
        int attackTimer;
        #endregion

        public override void SetDefaults()
        {
            NPC.width = 99;
            NPC.height = 119;
            NPC.boss = true;
            Music = MusicID.LunarBoss;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 20;
            NPC.lifeMax = 1000000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
        }

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 3000000;
                NPC.defense = 80;
                NPC.damage = 114;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 4000000;
                NPC.defense = 85;
                NPC.damage = 116;
            }
            else
            {
                NPC.lifeMax = 2000000;
                NPC.defense = 75;
                NPC.damage = 112;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {

            }
        }

        private Player player;

        public override bool PreAI()
        {
            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }
            Timer++;
            if (Timer >= 0)
            {
                Vector2 StartPosition = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float DirectionX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = Speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (NPC.velocity.X < DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X + Acceleration;
                    if (NPC.velocity.X < 0 && DirectionX > 0)
                        NPC.velocity.X = NPC.velocity.X + Acceleration;
                }
                else if (NPC.velocity.X > DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X - Acceleration;
                    if (NPC.velocity.X > 0 && DirectionX < 0)
                        NPC.velocity.X = NPC.velocity.X - Acceleration;
                }
                if (NPC.velocity.Y < DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                    if (NPC.velocity.Y < 0 && DirectionY > 0)
                        NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                }
                else if (NPC.velocity.Y > DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                    if (NPC.velocity.Y > 0 && DirectionY < 0)
                        NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                }
                if (Main.rand.NextBool(36))
                {
                    Vector2 StartPosition2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                    float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), StartPosition2.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                    NPC.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                    NPC.netUpdate = true;
                }
            }

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            var entitySource = NPC.GetSource_FromAI();

            #region Death Dialogue
            if (player.dead)
            {
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("Surprise, you have fallen before me!", 215, 95, 0);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("Did you like the improvements that I made?", 215, 95, 0);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("I was the first construct of the underworld, but not the first flame.", 215, 95, 0);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText(player.name + ", your mistakes don't get you far... Remember that!", 215, 95, 0);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("That was terrible...", 215, 95, 0);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Maybe if you were skilled enough, I wouldn't have called you out for skill issue right now...", 215, 95, 0);
                }
                NPC.active = false;
            }
            #endregion

            attackTimer++;

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (NPC.life < NPC.lifeMax / 1.25f)
            {
                
            }
            if (NPC.life < NPC.lifeMax / 2)
            {
                if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120 || attackTimer == 125)
                {
                    int amountOfProjectiles = 2 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 200 || attackTimer == 205 || attackTimer == 210 || attackTimer == 215 || attackTimer == 220 || attackTimer == 235)
                {
                    int amountOfProjectiles = 4 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<FlamingSoul>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer >= 300)
                {
                    attackTimer = 0;
                }
            }
            if (NPC.life < NPC.lifeMax / 2.5f)
            {
                if (attackTimer == 310 || attackTimer == 315 || attackTimer == 320 || attackTimer == 325 || attackTimer == 330 || attackTimer == 335)
                {
                    int amountOfProjectiles = 2 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 410 || attackTimer == 415 || attackTimer == 420 || attackTimer == 425 || attackTimer == 430 || attackTimer == 435)
                {
                    int amountOfProjectiles = 4 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<FlamingSoul>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer >= 500)
                {
                    attackTimer = 0;
                }
            }
            else
            {
                if (attackTimer == 110 || attackTimer == 115 || attackTimer == 120 || attackTimer == 125 || attackTimer == 130 || attackTimer == 135)
                {
                    int amountOfProjectiles = 2 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 210 || attackTimer == 215 || attackTimer == 220 || attackTimer == 225 || attackTimer == 230 || attackTimer == 235)
                {
                    int amountOfProjectiles = 4 + Main.rand.Next(2);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<FlamingSoul>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer >= 300)
                {
                    attackTimer = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
