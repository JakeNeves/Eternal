using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.BossBarStyles;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Pets;
using Eternal.Content.Items.Potions;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Items.Weapons.Summon;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Explosion;
using Eternal.Content.Projectiles.Misc;
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

namespace Eternal.Content.NPCs.Boss.CosmicApparition
{
    [AutoloadBossHead]
    public class CosmicApparition : ModNPC
    {
        public override void Load()
        {
            string texture = "Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition2_Head_Boss";
            secondStageHeadSlot = Mod.AddBossHeadTexture(texture, -1);
        }

        public override void BossHeadSlot(ref int index)
        {
            int slot = secondStageHeadSlot;
            if (phase == 1 && slot != -1)
            {
                index = slot;
            }
        }


        int phase = 0;
        int attackTimerP1Max = 300;
        int attackTimerP2Max = 600;
        int teleportTimer;

        int expTimer = 0;

        static int secondStageHeadSlot = -1;

        bool canTeleport = true;

        ref float attackTimerP1 => ref NPC.localAI[1];
        ref float attackTimerP2 => ref NPC.localAI[1];

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.TrailCacheLength[NPC.type] = 12;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;

            #region debuff immunity
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.DryadsWardDebuff] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.CursedInferno] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BetsysCurse] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            #endregion
        }

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 46;
            NPC.lifeMax = 120000;
            NPC.damage = 75;
            NPC.defense = 60;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            if (!Main.dedServ) {
                if (RiftSystem.isRiftOpen)
                    Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/GhostFromAnotherWorld");
                else
                    Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/ApparitionalAccumulation");
            }
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CosmicApparitionHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CosmicApparitionDeath");
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.alpha = 0;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
            NPC.npcSlots = 10;
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedCosmicApparition, -1);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A lost soul, seeking vengance on anything that attacks. Perhaps it seeks something from the Fallen Comet, yet this atoned spirit of an agglomerated horror has been witnessed by very little travelers!")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<ReminantHead>(), 4));

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CosmicApparitionBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalMatter>(), 2, 6, 12));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarmetalBar>(), 2, 6, 12));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InterstellarScrapMetal>(), 2, 6, 12));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalStave>(), 4));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalDisk>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Starfall>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Vexation>(), 1));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
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

        public override bool PreAI()
        {
            Player player = Main.player[NPC.target];

            player.AddBuff(BuffID.Horrified, 1, false);

            if (DifficultySystem.hellMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    player.AddBuff(BuffID.Obstructed, 1, false);
                }
            }

            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.02f;

            float speed = 30f;
            float acceleration = 0.15f;
            if (Main.expertMode)
            {
                speed = 45f;
                acceleration = 0.30f;
            }
            else if (DifficultySystem.hellMode)
            {
                speed = 60f;
                acceleration = 0.45f;
            }
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
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                {
                    EternalBossBarOverlay.SetTracked("Ambusher from the Fallen Comet", NPC);
                    EternalBossBarOverlay.visible = true;
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            Vector2 playerPosition = Main.player[NPC.target].position;

            NPC.spriteDirection = NPC.direction = NPC.Center.X < player.Center.X ? -1 : 1;

            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.04f;
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()) && DifficultySystem.hellMode)
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if (NPC.life < NPC.lifeMax / 2 && phase < 1)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CosmicApparitionAnger"), NPC.position);

                phase = 1;
            }

            if (NPC.ai[3] > 0f)
            {
                var entitySource = NPC.GetSource_Death();

                canTeleport = false;
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;
                expTimer++;

                if (expTimer >= 5)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<CosmicSpirit>(), 0, 0f, Main.myPlayer);
                    expTimer = 0;
                }

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                if (Main.rand.NextBool(5) && NPC.ai[3] < 180f)
                {
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, DustID.PinkTorch, 0f, 0f, 0, default(Color), 1f)];
                        dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width * 1.5f, NPC.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
                    }
                }

                if (NPC.ai[3] >= 180f)
                {
                    NPC.life = 0;
                    if (!DownedBossSystem.downedCosmicApparition)
                    {
                        DownedBossSystem.downedCosmicApparition = true;
                        Main.NewText("The cosmic entities have been empowered...", 120, 160, 240);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CosmicApparitionSoul>(), 0, 0f, Main.myPlayer);
                    }

                    if (RiftSystem.isRiftOpen && !DownedBossSystem.downedRiftCosmicApparition)
                    {
                        DownedBossSystem.downedRiftCosmicApparition = true;
                    }
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }

            if (canTeleport)
            {
                teleportTimer++;
            }

            if (Main.expertMode)
            {
                if (teleportTimer == 200)
                {
                    if (canTeleport)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        NPC.position.X = playerPosition.X + Main.rand.Next(-600, 600);
                        NPC.position.Y = playerPosition.Y + Main.rand.Next(-600, 600);
                    }
                    teleportTimer = 0;
                }
            }
            else if (DifficultySystem.hellMode)
            {
                if (teleportTimer == 150)
                {
                    if (canTeleport)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        NPC.position.X = playerPosition.X + Main.rand.Next(-600, 600);
                        NPC.position.Y = playerPosition.Y + Main.rand.Next(-600, 600);
                    }
                    teleportTimer = 0;
                }
            }
            else
            {
                if (teleportTimer == 400)
                {
                    if (canTeleport)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        NPC.position.X = playerPosition.X + Main.rand.Next(-600, 600);
                        NPC.position.Y = playerPosition.Y + Main.rand.Next(-600, 600);
                    }
                    teleportTimer = 0;
                }
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                AI_CosmicApparition_Attacks_Phase2();
            }
            else
            {
                AI_CosmicApparition_Attacks_Phase1();
            }
        }

        private void AI_CosmicApparition_Attacks_Phase1()
        {
            var entitySource = NPC.GetSource_FromAI();

            attackTimerP1++;
            if (attackTimerP1 > attackTimerP1Max)
            {
                attackTimerP1 = 0;
            }

            switch (attackTimerP1)
            {
                case 100:
                    NPC.NewNPC(entitySource, (int)NPC.position.X + 120, (int)NPC.position.Y, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X - 120, (int)NPC.position.Y, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y + 120, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y - 120, ModContent.NPCType<CosmicApex>());
                    break;
                case 250:
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (DifficultySystem.hellMode)
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<CosmicPierce>(), NPC.damage / 2, 0f);
                            }
                        }
                        else if (Main.expertMode)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<CosmicPierce>(), NPC.damage / 2, 0f);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<CosmicPierce>(), NPC.damage / 2, 0f);
                            }
                        }
                    }
                    break;
            }
        }

        private void AI_CosmicApparition_Attacks_Phase2()
        {
            var entitySource = NPC.GetSource_FromAI();

            attackTimerP2++;
            if (attackTimerP2 > attackTimerP2Max)
            {
                attackTimerP2 = 0;
            }

            switch (attackTimerP1)
            {
                case 100:
                    NPC.NewNPC(entitySource, (int)NPC.position.X + 240, (int)NPC.position.Y, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X - 240, (int)NPC.position.Y, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y + 240, ModContent.NPCType<CosmicApex>());
                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y - 240, ModContent.NPCType<CosmicApex>());
                    break;
                case 250:
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (DifficultySystem.hellMode)
                        {
                            for (int i = 0; i < 18; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0f);
                            }
                        }
                        else if (Main.expertMode)
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0f);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Projectile.NewProjectile(entitySource, NPC.Center, speed, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0f);
                            }
                        }
                    }
                    break;
                case 450:
                    for (int i = 0; i < Main.rand.Next(4, 6); i++)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.position.X + Main.rand.Next(-240, 240), (int)NPC.position.Y + Main.rand.Next(-240, 240), ModContent.NPCType<CosmicDecoy>());
                    }
                    break;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("CosmicApparitionHead").Type;
                int gore2 = Mod.Find<ModGore>("CosmicApparitionBody").Type;
                int gore3 = Mod.Find<ModGore>("CosmicApparitionArm").Type;

                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
            }
            else
            {
                for (int k = 0; k < 10.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            if (phase == 1)
            {
                //tf does this supposed to mean
                int num159 = 1;
                float num160 = 0f;
                int num161 = num159;
                SpriteEffects spriteEffects = SpriteEffects.None;
                Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
                Texture2D texture2D4 = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition2").Value;
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

            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (phase == 1 && Main.expertMode)
            {
                // Brin of Cthulu things
                Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
                Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition2").Value;
                float num66 = 0f;
                Vector2 vector11 = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[NPC.type] / 2));
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                Microsoft.Xna.Framework.Rectangle frame6 = NPC.frame;
                Microsoft.Xna.Framework.Color alpha15 = NPC.GetAlpha(color9);
                float alpha = 1.25f * (1f - (float)NPC.life / (float)NPC.lifeMax);
                alpha *= alpha;
                alpha = Math.Min(alpha, 1);
                alpha15.R = (byte)((float)alpha15.R * alpha);
                alpha15.G = (byte)((float)alpha15.G * alpha);
                alpha15.B = (byte)((float)alpha15.B * alpha);
                alpha15.A = (byte)((float)alpha15.A * alpha);
                for (int num213 = 0; num213 < 4; num213++)
                {
                    Vector2 position9 = NPC.position;
                    float num214 = Math.Abs(NPC.Center.X - Main.player[Main.myPlayer].Center.X);
                    float num215 = Math.Abs(NPC.Center.Y - Main.player[Main.myPlayer].Center.Y);
                    if (num213 == 0 || num213 == 2)
                    {
                        position9.X = Main.player[Main.myPlayer].Center.X + num214;
                    }
                    else
                    {
                        position9.X = Main.player[Main.myPlayer].Center.X - num214;
                    }
                    position9.X -= (float)(NPC.width / 2);
                    if (num213 == 0 || num213 == 1)
                    {
                        position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
                    }
                    else
                    {
                        position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
                    }
                    position9.Y -= (float)(NPC.height / 2);
                    Main.spriteBatch.Draw(texture, new Vector2(position9.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)texture.Width * NPC.scale / 2f + vector11.X * NPC.scale, position9.Y - Main.screenPosition.Y + (float)NPC.height - (float)texture.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
                }
                Main.spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)texture.Width * NPC.scale / 2f + vector11.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)texture.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
