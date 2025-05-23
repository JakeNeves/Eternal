﻿using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Projectiles.Boss;
using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Content.Projectiles.Enemy;

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperor : ModNPC
    {
        int dialogueTimer = 0;

        bool spawnedClones = false;
        bool dialogue = false;
        bool firstAttack = false;

        bool canAttack = false;

        int AICosmicEmperorShootTime = 4;
        int AICosmicEmperorShootRate()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 6;
            else if (Main.expertMode)
                rate = 8;
            else
                rate = 10;

            return rate;
        }

        static int AICosmicEmperorLaserShotRateMax()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 4;
            else if (Main.expertMode)
                rate = 5;
            else
                rate = 6;

            return rate;
        }

        int AICosmicEmperorLaserShotRate = AICosmicEmperorLaserShotRateMax();

        float AICosmicEmperorProjectileRotation = MathHelper.PiOver2;

        ref float AttackTimer => ref NPC.ai[1];

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 54;
            NPC.height = 56;
            NPC.lifeMax = 1000000;
            NPC.defense = 75;
            NPC.damage = 30;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            NPC.boss = true;
            Music = MusicID.OtherworldlyLunarBoss;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 6;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            canAttack = true;

            Player target = Main.player[NPC.target];

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                var player = Main.player[i];
                if (!player.active)
                    continue;

                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                if (Main.rand.NextBool(2))
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_OnHurt(player), NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                }
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override bool PreAI()
        {
            #region boundry circle
            int maxDist = 1500;

            // ripped from another mod, credit to the person who wrote this
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset, 0, 0, DustID.Shadowflame, 0, 0, 100)];
                dust.noGravity = true;
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead && Vector2.Distance(player.Center, NPC.Center) > maxDist)
                {
                    Vector2 toTarget = new Vector2(NPC.Center.X - player.Center.X, NPC.Center.Y - player.Center.Y);
                    toTarget.Normalize();
                    float speed = Vector2.Distance(player.Center, NPC.Center) > maxDist + 500 ? 1f : 0.5f;
                    player.velocity += toTarget * 0.5f;

                    player.dashDelay = 2;
                    player.grappling[0] = -1;
                    player.grapCount = 0;
                    for (int p = 0; p < Main.projectile.Length; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                        {
                            Main.projectile[p].Kill();
                        }
                    }
                }
            }
            int maxdusts = 6;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 80;
                float dustSpeed = 8;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.DemonTorch, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
            #endregion
            return true;
        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;

                if (!dialogue)
                    dialogueTimer++;
                else
                {
                    dialogueTimer = 0;
                }
                switch (dialogueTimer)
                {
                    case 100:
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "...", dramatic: true);
                        Main.NewText("...", 150, 36, 120);
                        break;
                    case 200:
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "No...", dramatic: true);
                        Main.NewText("No...", 150, 36, 120);
                        break;
                    case 300:
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "Not like this!", dramatic: true);
                        Main.NewText("Not like this!", 150, 36, 120);
                        break;
                    case 400:
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "I just have one more thing I'm gonna pull off, before I give in all that I've got....", dramatic: true);
                        Main.NewText("I just have one more thing I'm gonna pull off, before I give in all that I've got...", 150, 36, 120);
                        break;
                    case 500:
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "Clones, cut them down, tear them apart, punish them once in for all!...", dramatic: true);
                        Main.NewText("Clones, cut them down, tear them apart, punish them once in for all!", 150, 36, 120);
                        spawnedClones = true;
                        for (int i = 0; i < 6; ++i)
                        {
                            NPC.NewNPC(entitySource, (int)NPC.position.X + Main.rand.Next(-200, 200), (int)NPC.position.Y + Main.rand.Next(-200, 200), ModContent.NPCType<CosmicEmperorClone>());
                        }
                        dialogue = true;
                        break;
                }
                if (spawnedClones)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<CosmicEmperorClone>()))
                    {
                        CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "Useless, I gues this is my last resort, I'll really have take matter into my own hands now!", dramatic: true);
                        Main.NewText("Useless, I gues this is my last resort, I'll really have take matter into my own hands now!", 150, 36, 120);
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<CosmicEmperorP2>());
                        NPC.active = false;
                    }
                }

                if (NPC.ai[3] >= 500f && !NPC.AnyNPCs(ModContent.NPCType<CosmicEmperorClone>()))
                {
                    NPC.life = 0;
                    NPC.checkDead();
                }

                return;
            }

            if (canAttack)
            {
                if (NPC.life < NPC.lifeMax / 1.25)
                    AI_Cosmic_Emperor_Attacks_Phase2();
                else if (NPC.life < NPC.lifeMax / 2)
                    AI_Cosmic_Emperor_Attacks_Phase3();
                else
                    AI_Cosmic_Emperor_Attacks_Phase1();
            }
        }

        private void AI_Cosmic_Emperor_Attacks_Phase1()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 circDir = new Vector2(0f, 15f);

            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (canAttack)
                AttackTimer++;

            if (AttackTimer >= 100 && AttackTimer < 250)
            {
                AICosmicEmperorLaserShotRate--;

                if (AICosmicEmperorLaserShotRate <= 0)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 100;
                    }

                    AICosmicEmperorLaserShotRate = AICosmicEmperorLaserShotRateMax();
                }
            }

            if (AttackTimer >= 300 && AttackTimer < 400)
            {
                AICosmicEmperorLaserShotRate--;

                if (AICosmicEmperorLaserShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            circDir = Utils.RotatedBy(circDir, 0.15, new Vector2());
                            int proj = Projectile.NewProjectile(entitySource, NPC.Center, circDir, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 0.0f);

                            Main.projectile[proj].tileCollide = false;
                            Main.projectile[proj].timeLeft = 100;
                        }
                    }

                    AICosmicEmperorLaserShotRate = AICosmicEmperorLaserShotRateMax();
                }
            }

            if (AttackTimer >= 450 && AttackTimer < 500)
            {
                AICosmicEmperorLaserShotRate--;

                if (AICosmicEmperorLaserShotRate <= 0)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.02f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.02f;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicEmperorKnife>(), NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 100;
                    }

                    AICosmicEmperorLaserShotRate = AICosmicEmperorLaserShotRateMax();
                }
            }

            if (AttackTimer >= 600 && AttackTimer < 1000)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.01f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.Center);

                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 10).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer >= 1100 && AttackTimer < 1600)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.01f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 5).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel, ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.Pi), ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.EyeLaser, NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].friendly = false;
                            Main.projectile[i[l]].hostile = true;
                            Main.projectile[i[l]].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer > 2100)
                AttackTimer = 0;
        }

        private void AI_Cosmic_Emperor_Attacks_Phase2()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (canAttack)
                AttackTimer++;

            if (AttackTimer > 2000)
                AttackTimer = 0;
        }

        private void AI_Cosmic_Emperor_Attacks_Phase3()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (canAttack)
                AttackTimer++;

            if (AttackTimer > 2000)
                AttackTimer = 0;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
