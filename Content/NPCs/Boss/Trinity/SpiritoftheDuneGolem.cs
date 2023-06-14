using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Items.BossBags;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    //[AutoloadBossHead]
    public class SpiritoftheDuneGolem : ModNPC
    {
        private Player player;

        #region Fundimentals
        int attackTimer;
        int Phase;
        int Timer;
        int frameNum;
        int DeathTimer;

        bool isDead = false;
        bool dontKillyet = false;

        const float Speed = 12f;
        const float Acceleration = 0.2f;
        #endregion

        bool phase2Init = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spirit of the Dune Golem");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1000000;
            NPC.damage = 95;
            NPC.defense = 60;
            NPC.knockBackResist = -1f;
            NPC.width = 106;
            NPC.height = 106;
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/TrinitySpiritHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            //NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/TrinitySpiritDeath");
            //NPC.DeathSound = SoundID.NPCDeath3;
            Music = MusicID.LunarBoss;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.DesertTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
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

        public override bool PreAI()
        {
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            if (Phase == 1)
            {
                if (!phase2Init)
                {
                    SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
                    phase2Init = true;
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
                        float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0f)), StartPosition2.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                        NPC.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                        NPC.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                        NPC.netUpdate = true;
                    }
                }
            }
            else
            {
                float speed = 36.25f;
                float acceleration = 0.12f;
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
            }

            return true;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (!isDead)
            {
                #region attacks
                attackTimer++;

                if (NPC.life < NPC.lifeMax / 2)
                {
                    Phase = 1;
                    NPC.rotation += NPC.velocity.X * 0.1f;
                }
                else
                {
                    NPC.rotation = NPC.velocity.X * 0.03f;
                }

                if (Phase == 1)
                {

                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120 || attackTimer == 125 || attackTimer == 130 || attackTimer == 135 || attackTimer == 140 || attackTimer == 145 || attackTimer == 150)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                            float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpike>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    if (attackTimer == 200 || attackTimer == 205 || attackTimer == 210 || attackTimer == 215 || attackTimer == 220 || attackTimer == 225 || attackTimer == 230 || attackTimer == 235 || attackTimer == 240 || attackTimer == 245 || attackTimer == 250 || attackTimer == 260 || attackTimer == 265 || attackTimer == 270 || attackTimer == 275 || attackTimer == 280 || attackTimer == 285 || attackTimer == 290 || attackTimer == 295 || attackTimer == 300)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.Center);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSpark>(), NPC.damage, 0f, Main.myPlayer);
                    }
                    if (attackTimer == 300)
                    {
                        
                    }
                    if (attackTimer == 500)
                    {
                        attackTimer = 0;
                    }
                }
                else
                {

                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120 || attackTimer == 125)
                    {
                        for (int i = 0; i < 1; ++i)
                        {
                            SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                            float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpike>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    if (attackTimer == 200 || attackTimer == 205 || attackTimer == 210 || attackTimer == 215 || attackTimer == 220 || attackTimer == 225 || attackTimer == 230 || attackTimer == 235 || attackTimer == 240 || attackTimer == 245 || attackTimer == 250)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpark>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                    if (attackTimer == 300)
                    {

                    }
                    if (attackTimer == 500)
                    {
                        attackTimer = 0;
                    }
                }
                #endregion
            }
            else
            {
                DeathTimer++;
                if (DeathTimer > 5)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    NPC.life = 1;
                    NPC.dontTakeDamage = true;
                    NPC.rotation = DeathTimer;
                }
                if (DeathTimer >= 100)
                {
                    NPC.dontTakeDamage = false;
                    dontKillyet = true;
                    player.ApplyDamageToNPC(NPC, 9999, 0, 0, false);

                    for (int i = 0; i < 75; i++)
                    {
                        Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 75 * i)) * 45;
                        Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.DesertTorch);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Normalize(position - NPC.Center) * 12;
                        dust.noLight = false;
                        dust.fadeIn = 1f;
                    }

                    for (int i = 0; i < 8; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSpark>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}

