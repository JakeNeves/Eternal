using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Pets;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.TheGlare
{
    [AutoloadBossHead]
    public class TheGlare : ModNPC
    {
        bool justSpawnedHeart = false;

        static int aiGlarePhase1ShootRateMax = 6;
        int aiGlarePhase1ShootRate = aiGlarePhase1ShootRateMax;

        static int aiGlarePhase2ShootRateMax = 2;
        int aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;

        int aiInfernitoShootTime = 4;

        int AiGlareShootRate()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 4;
            else if (Main.expertMode)
                rate = 6;
            else
                rate = 8;

            return rate;
        }

        float aiGlareProjectileRotation = MathHelper.PiOver2;

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "Eternal/Content/NPCs/Boss/TheGlare/TheGlare_Preview"
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);

            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void Load()
        {
            string texture = "Eternal/Content/NPCs/Boss/TheGlare/TheGlareGehenna_Head_Boss";
            altHeadSlot = Mod.AddBossHeadTexture(texture, -1);
        }

        public override void BossHeadSlot(ref int index)
        {
            int slot = altHeadSlot;
            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna && slot != -1)
            {
                index = slot;
            }
        }

        static int altHeadSlot = -1;

        ref float AttackTimer => ref NPC.ai[1];

        public override void SetDefaults()
        {
            NPC.width = 134;
            NPC.height = 134;
            NPC.aiStyle = -1;
            NPC.damage = 24;
            NPC.defense = 30;
            NPC.lifeMax = 12000;
            NPC.lavaImmune = true;
            NPC.knockBackResist = -1f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.boss = true;
            NPC.npcSlots = 6;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/Infamy");
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type, ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void OnKill()
        {
            NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TheGlareDefeated>());

            NPC.SetEventFlagCleared(ref DownedBossSystem.downedGlare, -1);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new FlavorTextBestiaryInfoElement("A giant mask with a rather eerie glow within...")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, 0, 0, default(Color), 1f);
                }
            }
            else
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PurpleTorch, 0, 0, 0, default(Color), 1f);
                }
            }
        }

        public override bool PreAI()
        {
            if (!justSpawnedHeart)
            {
                int heart = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GlareHeart>());
                justSpawnedHeart = true;
            }

            float speed;

            if (DifficultySystem.hellMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                    speed = 60f;
                else
                    speed = 30f;
            }
            else if (Main.expertMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                    speed = 40f;
                else
                    speed = 20f;
            }
            else
            {
                if (NPC.life < NPC.lifeMax / 2)
                    speed = 20f;
                else
                    speed = 10f;
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

            return true;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            var entitySource = NPC.GetSource_FromAI();

            AttackTimer++;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 2.15f);

            if (!NPC.AnyNPCs(ModContent.NPCType<GlareHeart>()))
            {
                NPC.dontTakeDamage = false;

                if (NPC.life < NPC.lifeMax / 2)
                    AI_Glare_Attacks_Phase2();
                else
                    AI_Glare_Attacks_Phase1();
            }
            else
                NPC.dontTakeDamage = true;
        }

        private void AI_Glare_Attacks_Phase1()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            Vector2 circDir = new Vector2(0f, 45f);

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            AttackTimer++;

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna) // Harder attacks for the Gehenna variant
            {
                if (AttackTimer >= 250 && AttackTimer < 300)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase1ShootRate--;

                    if (aiGlarePhase1ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                        aiGlarePhase1ShootRate = aiGlarePhase1ShootRateMax;
                    }
                }

                if (AttackTimer == 350)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                    }
                }

                if (AttackTimer == 400)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y,-12f, -12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.InfernoHostileBolt, NPC.damage, 0);
                    }
                }

                if (AttackTimer >= 425 && AttackTimer < 450)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase1ShootRate--;

                    if (aiGlarePhase1ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                        }

                        aiGlarePhase1ShootRate = aiGlarePhase1ShootRateMax;
                    }
                }

                if (AttackTimer == 500)
                {
                    if (!Main.dedServ)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                    }
                }

                if (AttackTimer == 550)
                {
                    if (!Main.dedServ)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.DeathLaser, NPC.damage / 4, 0);
                    }
                }

                if (AttackTimer == 650)
                {
                    if (!Main.dedServ)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage / 2, 0);
                    }
                }
            }
            else // Attacks for the default Mausoleum variant
            {
                if (AttackTimer >= 250 && AttackTimer < 300)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase1ShootRate--;

                    if (aiGlarePhase1ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                        aiGlarePhase1ShootRate = aiGlarePhase1ShootRateMax;
                    }
                }

                if (AttackTimer == 400)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.Shadowflames, NPC.damage, 0);
                    }
                }

                if (AttackTimer == 450)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.Shadowflames, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.Shadowflames, NPC.damage, 0);
                    }
                }

                if (AttackTimer >= 500 && AttackTimer < 575)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase1ShootRate--;

                    if (aiGlarePhase1ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                        }

                        aiGlarePhase1ShootRate = aiGlarePhase1ShootRateMax;
                    }
                }

                if (AttackTimer == 600)
                {
                    if (!Main.dedServ)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.EyeLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.EyeLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.EyeLaser, NPC.damage / 4, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.EyeLaser, NPC.damage / 4, 0);
                    }
                }

                if (AttackTimer == 650)
                {
                    if (!Main.dedServ)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.ShadowBeamHostile, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.ShadowBeamHostile, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.ShadowBeamHostile, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.ShadowBeamHostile, NPC.damage / 2, 0);
                    }
                }
            }

            if (AttackTimer > 700)
                AttackTimer = 0;
        }

        private void AI_Glare_Attacks_Phase2()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            Vector2 circDir = new Vector2(0f, 45f);

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            AttackTimer++;

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna) // Harder attacks for the Gehenna variant
            {
                if (AttackTimer >= 200 && AttackTimer < 400)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int laser = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                            Main.projectile[laser].tileCollide = false;
                            Main.projectile[laser].timeLeft = 250;
                        }

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }
                }

                if (AttackTimer >= 250 && AttackTimer < 450)
                {
                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.InfernoHostileBolt, NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }


                }

                if (AttackTimer >= 500 && AttackTimer < 700)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.DeathLaser, NPC.damage, 0);
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.DeathLaser, NPC.damage, 0);
                        }

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }
                }
            }

            if (AttackTimer == 750)
            {
                for (int i = 0; i < 2 + Main.rand.Next(2); i++)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 4, 1, Main.myPlayer, 0, 0);
            }

            if (AttackTimer >= 1000 && AttackTimer < 1200)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiGlareProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiGlareShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(aiGlareProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.DeathLaser, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                    }
                }
            }

            if (AttackTimer >= 1400 && AttackTimer < 1500)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiGlareProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiGlareShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(-aiGlareProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.InfernoHostileBolt, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.InfernoHostileBolt, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                    }
                }
            }
            else // Attacks for the default Mausoleum variant
            {
                if (AttackTimer >= 200 && AttackTimer < 400)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int laser = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.ShadowBeamHostile, NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                            Main.projectile[laser].tileCollide = false;
                            Main.projectile[laser].timeLeft = 250;
                        }

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }
                }

                if (AttackTimer >= 250 && AttackTimer < 450)
                {
                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.Shadowflames, NPC.damage / 4, 1, Main.myPlayer, 0, 0);

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }

                    
                }

                if (AttackTimer >= 500 && AttackTimer < 700)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;

                    aiGlarePhase2ShootRate--;

                    if (aiGlarePhase2ShootRate < 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.EyeLaser, NPC.damage, 0);
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.EyeLaser, NPC.damage, 0);
                        }

                        aiGlarePhase2ShootRate = aiGlarePhase2ShootRateMax;
                    }
                }
            }

            if (AttackTimer == 750)
            {
                for (int i = 0; i < 2 + Main.rand.Next(2); i++)
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.Shadowflames, NPC.damage / 4, 1, Main.myPlayer, 0, 0);
            }

            if (AttackTimer >= 1000 && AttackTimer < 1200)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiGlareProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiGlareShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(aiGlareProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.EyeLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.EyeLaser, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                    }
                }
            }

            if (AttackTimer >= 1400 && AttackTimer < 1500)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiGlareProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiGlareShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(-aiGlareProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.Shadowflames, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.Shadowflames, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                    }
                }
            }

            if (AttackTimer > 1600)
                AttackTimer = 0;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // TODO: Glare Loot Table

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TheGlareBag>()));

            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<HauntedVisage>(), 4));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                //tf does this supposed to mean
                int num159 = 1;
                float num160 = 0f;
                int num161 = num159;
                SpriteEffects spriteEffects = SpriteEffects.None;
                Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
                Texture2D texture2D4 = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/TheGlare/TheGlareGehenna").Value;
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
                return false;
            }

            return true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
