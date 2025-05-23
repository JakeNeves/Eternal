﻿using Eternal.Common.Systems;
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
using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Eternal.Content.NPCs.Boss.DuneGolem
{
    [AutoloadBossHead]
    public class DuneGolem : ModNPC
    {
        private Player player;

        #region Fundimentals
        ref float AttackTimer => ref NPC.ai[1];

        int Phase;
        int Timer;
        int frameNum;
        int DeathTimer;

        const float Speed = 12f;
        const float Acceleration = 0.2f;
        #endregion

        bool isDead = false;
        bool dontKillyet = false;
        bool phase2Init = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 6000;
            NPC.damage = 6;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 106;
            NPC.height = 106;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/HauntedTombfromtheDunes");
            }
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            int gore1 = Mod.Find<ModGore>("DuneGolem1").Type;
            int gore2 = Mod.Find<ModGore>("DuneGolem2").Type;
            int gore3 = Mod.Find<ModGore>("DuneGolem3").Type;
            int gore4 = Mod.Find<ModGore>("DuneGolem4").Type;

            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore1);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore2);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore3);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore4);

            NPC.SetEventFlagCleared(ref DownedBossSystem.downedDuneGolem, -1);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

                new FlavorTextBestiaryInfoElement("A possessed idol, perhaps it was protecting the tomb of a former ruler of the dunes")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }

            for (int k = 0; k < 5; k++)
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.DesertTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DuneGolemBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MalachiteShard>(), minimumDropped: 30, maximumDropped: 60));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
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
                    if (!Main.dedServ)
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
                float speed;

                if (DifficultySystem.hellMode)
                {
                    speed = 10f;
                }
                else if (Main.expertMode)
                {
                    speed = 8f;
                }
                else
                {
                    speed = 6.5f;
                }

                if (!player.ZoneDesert)
                {
                    speed = 20f;
                }

                float acceleration = 0.10f;
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

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (NPC.AnyNPCs(ModContent.NPCType<DunePylon>()))
            {
                NPC.dontTakeDamage = true;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0;
            }
            else if (!NPC.AnyNPCs(ModContent.NPCType<DunePylon>()))
            {
                NPC.dontTakeDamage = false;
            }

            #region attacks
            AttackTimer++;

            if (NPC.life < NPC.lifeMax / 2)
            {
                Phase = 1;
                frameNum = 1;
                NPC.rotation += NPC.velocity.X * 0.1f;
            }
            else
            {
                NPC.rotation = NPC.velocity.X * 0.03f;
            }

            if (Phase == 1)
            {

                if (AttackTimer == 100 || AttackTimer == 105 || AttackTimer == 110 || AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125 || AttackTimer == 130 || AttackTimer == 135 || AttackTimer == 140 || AttackTimer == 145 || AttackTimer == 150)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);

                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpike>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (AttackTimer == 200 || AttackTimer == 205 || AttackTimer == 210 || AttackTimer == 215 || AttackTimer == 220 || AttackTimer == 225 || AttackTimer == 230 || AttackTimer == 235 || AttackTimer == 240 || AttackTimer == 245 || AttackTimer == 250 || AttackTimer == 260 || AttackTimer == 265 || AttackTimer == 270 || AttackTimer == 275 || AttackTimer == 280 || AttackTimer == 285 || AttackTimer == 290 || AttackTimer == 295 || AttackTimer == 300)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.Center);

                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSpark>(), NPC.damage, 0f, Main.myPlayer);
                }
                if (AttackTimer == 300)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<DunePylon>()))
                    {
                        if (DifficultySystem.hellMode)
                        {
                            int amountOfPylons = Main.rand.Next(6, 10);
                            for (int i = 0; i < amountOfPylons; ++i)
                            {
                                NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<DunePylon>());
                            }
                        }
                        else
                        {
                            int amountOfPylons = Main.rand.Next(4, 8);
                            for (int i = 0; i < amountOfPylons; ++i)
                            {
                                NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<DunePylon>());
                            }
                        }
                    }
                }
                if (AttackTimer == 500)
                {
                    AttackTimer = 0;
                }
            }
            else
            {

                if (AttackTimer == 100 || AttackTimer == 105 || AttackTimer == 110 || AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125)
                {
                    for (int i = 0; i < 1; ++i)
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);

                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpike>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (AttackTimer == 200 || AttackTimer == 205 || AttackTimer == 210 || AttackTimer == 215 || AttackTimer == 220 || AttackTimer == 225 || AttackTimer == 230 || AttackTimer == 235 || AttackTimer == 240 || AttackTimer == 245 || AttackTimer == 250)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<DuneSpark>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                }
                if (AttackTimer == 300)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<DunePylon>()))
                        {
                        if (DifficultySystem.hellMode)
                        {
                            int amountOfPylons = Main.rand.Next(3, 6);
                            for (int i = 0; i < amountOfPylons; ++i)
                            {
                                NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<DunePylon>());
                            }
                        }
                        else
                        {
                            int amountOfPylons = Main.rand.Next(2, 4);
                            for (int i = 0; i < amountOfPylons; ++i)
                            {
                                NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<DunePylon>());
                            }
                        }
                    }
                }
                if (AttackTimer == 500)
                {
                    AttackTimer = 0;
                }
            }
            #endregion

            if (isDead)
            {
                entitySource = NPC.GetSource_Death();

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                DeathTimer++;
                if (DeathTimer > 5)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    NPC.dontTakeDamage = true;
                    NPC.rotation += 0.1f;
                }
                if (DeathTimer >= 200)
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

                    if (!DownedBossSystem.downedDuneGolem)
                    {
                        DownedBossSystem.downedDuneGolem = true;
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

