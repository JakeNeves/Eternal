using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Enemy;
using Eternal.Content.Projectiles.Explosion;
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

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperorP2 : ModNPC
    {
        ref float AttackTimer => ref NPC.localAI[1];

        int DialogueDeathTimer = 0;
        int deathExplosionTimer = 0;

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

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "Eternal/Content/NPCs/Boss/CosmicEmperor/CosmicEmperor"
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Hier to the thone, the saviour of the stars. Very well known for his cruel tyranny.")
            });
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 800000;
            NPC.width = 62;
            NPC.height = 56;
            NPC.damage = 50;
            NPC.defense = 90;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noTileCollide = true;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/Tyranicide");
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CosmicEmperorDeath");
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedCosmicEmperor, -1);
        }

        /*public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (damage > NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("No!", 150, 36, 120);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("I shall not tolerate such action...", 150, 36, 120);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("What is wrong with you?", 150, 36, 120);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText("You think your black magic can withstand my potental?", 150, 36, 120);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("What an absolute cheater you are.", 150, 36, 120);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 150, 36, 120);
                }
                if (Main.rand.NextBool(7))
                {
                    Main.NewText("That did not penetrate me...", 150, 36, 120);
                }
                if (Main.rand.NextBool(8))
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 150, 36, 120);
                }

                damage = 0;
            }
        }*/

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.rand.NextBool(2))
            {
                int num117 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, ModContent.DustType<Dusts.CosmicSpirit>(), NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num117].noGravity = true;
                Main.dust[num117].velocity.X *= 1f;
                Main.dust[num117].velocity.Y *= 1f;
            }
        }

        public override bool PreAI()
        {
            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity.Y = 50;
            }

            if (NPC.ai[0] == 0)
            {
                #region Flying Movement
                float speed = 60f;
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
            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            NPC.netUpdate = true;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;

                DialogueDeathTimer++;
                deathExplosionTimer++;

                if (deathExplosionTimer >= 5)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<CosmicSpirit>(), 0, 0f, Main.myPlayer);
                    deathExplosionTimer = 0;
                }

                NPC.velocity = new Vector2(Main.rand.NextFloat(-0.25f, 0.25f), Main.rand.NextFloat(-0.25f, 0.25f));
                
                if (DownedBossSystem.downedCosmicEmperor)
                {
                    switch (DialogueDeathTimer)
                    {
                        case 100:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "ARGH!", dramatic: true);
                            Main.NewText("ARGH!", 150, 36, 120);
                            break;
                        case 200:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "NO!", dramatic: true);
                            Main.NewText("NO!", 150, 36, 120);
                            break;
                        case 300:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "NOT LIKE THIS!", dramatic: true);
                            Main.NewText("NOT LIKE THIS!", 150, 36, 120);
                            break;
                        case 400:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "I HAVE BEEN DEFEATED, AGAIN!", dramatic: true);
                            Main.NewText("I HAVE BEEN DEFEATED, AGAIN!", 150, 36, 120);
                            break;
                        case 500:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "MY POWER IS DEPLETING...", dramatic: true);
                            Main.NewText("MY POWER IS DEPLETING...", 150, 36, 120);
                            break;
                        case 600:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "FATE IS MY OLDEST FRIEND", dramatic: true);
                            Main.NewText("FATE IS MY OLDEST FRIEND!", 150, 36, 120);
                            break;
                        case 700:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "GOODBYE!", dramatic: true);
                            Main.NewText("GOODBYE!", 150, 36, 120);
                            break;
                        case 750:
                            for (int k = 0; k < 15; k++)
                            {
                                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.CosmicSpirit>(), Main.rand.NextFloat(-1.75f, 1.75f), Main.rand.NextFloat(-1.75f, 1.75f), 0, default(Color), 1f);
                            }
                            NPC.dontTakeDamage = false;
                            break;
                    }

                }
                else
                {
                    switch (DialogueDeathTimer)
                    {
                        case 100:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "ARGH!", dramatic: true);
                            Main.NewText("ARGH!", 150, 36, 120);
                            break;
                        case 200:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "NO!", dramatic: true);
                            Main.NewText("NO!", 150, 36, 120);
                            break;
                        case 300:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "NOT LIKE THIS!", dramatic: true);
                            Main.NewText("NOT LIKE THIS!", 150, 36, 120);
                            break;
                        case 400:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "I HAVE BEEN DEFEATED!", dramatic: true);
                            Main.NewText("I HAVE BEEN DEFEATED!", 150, 36, 120);
                            break;
                        case 500:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "MY POWER IS DEPLETING...", dramatic: true);
                            Main.NewText("MY POWER IS DEPLETING...", 150, 36, 120);
                            break;
                        case 600:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "FATE IS MY OLDEST FRIEND", dramatic: true);
                            Main.NewText("FATE IS MY OLDEST FRIEND!", 150, 36, 120);
                            break;
                        case 700:
                            CombatText.NewText(NPC.Hitbox, new Color(150, 36, 120), "GOODBYE!", dramatic: true);
                            Main.NewText("GOODBYE!", 150, 36, 120);
                            break;
                        case 750:
                            for (int k = 0; k < 15; k++)
                            {
                                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.CosmicSpirit>(), Main.rand.NextFloat(-1.75f, 1.75f), Main.rand.NextFloat(-1.75f, 1.75f), 0, default(Color), 1f);
                            }
                            NPC.dontTakeDamage = false;
                            break;
                    }
                }

                if (NPC.ai[3] >= 750f)
                {
                    NPC.life = 0;

                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
            }
            else
            {
                if (NPC.life < NPC.lifeMax / 2)
                    AI_Cosmic_Emperor_Attacks_Phase_2();
                else
                    AI_Cosmic_Emperor_Attacks_Phase_1();
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.15f;
                NPC.EncourageDespawn(10);
                return;
            }
        }

        private void AI_Cosmic_Emperor_Attacks_Phase_1()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 circDir = new Vector2(0f, 15f);

            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            AttackTimer++;

            if (AttackTimer >= 200 && AttackTimer < 600)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.15f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 15).RotatedBy(AICosmicEmperorProjectileRotation);
                        var shootVel2 = new Vector2(0, -15).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel2, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 150;
                        }
                    }
                }
            }

            if (AttackTimer >= 800 && AttackTimer <= 900)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (AttackTimer == 900)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.Center);

                        for (int i = 0; i < 15; i++)
                        {
                            circDir = Utils.RotatedBy(circDir, 0.45, new Vector2());
                            int proj = Projectile.NewProjectile(entitySource, NPC.Center, circDir, ModContent.ProjectileType<Psyfireball>(), (int)(NPC.damage * 0.25f), 0.0f);

                            Main.projectile[proj].tileCollide = false;
                            Main.projectile[proj].timeLeft = 50;
                        }
                    }
                }
            }

            if (AttackTimer >= 1100 && AttackTimer <= 1200)
            {
                if (Main.rand.NextBool(6))
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_OnHurt(player), NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                }
            }

            if (AttackTimer >= 1400 && AttackTimer < 1600)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.01f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 10).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 150;
                        }
                    }
                }
            }

            if (AttackTimer > 2000)
                AttackTimer = 0;
        }

        private void AI_Cosmic_Emperor_Attacks_Phase_2()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            AttackTimer++;

            if (AttackTimer >= 200 && AttackTimer <= 400)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.15f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, -10).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer >= 600 && AttackTimer <= 1000)
            {
                if (Main.rand.NextBool(6))
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                }
            }

            if (AttackTimer == 1100)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    for (int i = 0; i < Main.rand.Next(4, 6); i++)
                        Projectile.NewProjectile(entitySource, NPC.Center + new Vector2(Main.rand.NextFloat(-100 ,100), Main.rand.NextFloat(-100, 100)), NPC.velocity = new Vector2(0f, 0f), ModContent.ProjectileType<CosmigeddonBomb>(), (int)(NPC.damage * 0.25f), 0f);
            }

            if (AttackTimer >= 1400 && AttackTimer < 1600)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(4))
                    {
                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-30f, -45f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.ShadowBeamHostile, NPC.damage / 2 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].tileCollide = false;
                    }

                    if (Main.rand.NextBool(8))
                    {
                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(30f, 45f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CosmicFireball>(), NPC.damage / 2 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 1800 && AttackTimer < 2000 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(12))
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-2, 2), (int)NPC.Center.Y + Main.rand.Next(-2, 2), ModContent.NPCType<GalaxiaWisp>());

                if (Main.rand.NextBool(3))
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);

                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-4, 4), -8, ModContent.ProjectileType<Psyfireball>(), (int)(NPC.damage * 0.25f), 0f);
                }
            }

            if (AttackTimer >= 2200 && AttackTimer <= 2400)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    AICosmicEmperorProjectileRotation += 0.05f;
                    if (--AICosmicEmperorShootTime <= 0)
                    {
                        AICosmicEmperorShootTime = AICosmicEmperorShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 10).RotatedBy(AICosmicEmperorProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.ShadowBeamHostile, (int)(NPC.damage * 0.25f), 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.ShadowBeamHostile, (int)(NPC.damage * 0.25f), 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.ShadowBeamHostile, (int)(NPC.damage * 0.25f), 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.ShadowBeamHostile, (int)(NPC.damage * 0.25f), 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer > 2500)
                AttackTimer = 0;
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
            potionType = ModContent.ItemType<PerfectHealingPotion>();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CosmicEmperorCapsule>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InterstellarMetal>(), 1, 4, 12));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmoniumFragment>(), 1, 3, 9));
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/CosmicEmperor/CosmicEmperorP2_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
