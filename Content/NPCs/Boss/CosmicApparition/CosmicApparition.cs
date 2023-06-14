using Eternal.Common.Systems;
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
        public int frameNum;
        int teleportTimer;
        int attackTimer;
        int generalAttackTimer;
        int moveTimer;
        int phase = 0;

        const float acceleration = 0.2f;
        const float speed = 14f;

        bool teleport = true;
        bool expert = Main.expertMode;

        public static int cAppGlobalFrame;

        public static int cAppAnimNumber;

        private Player player;

        bool phase2Warn = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.TrailCacheLength[NPC.type] = 12;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 46;
            NPC.lifeMax = 120000;
            NPC.damage = 75;
            NPC.defense = 60;
            NPC.knockBackResist = -1f;
            NPC.boss = true;
            if (RiftSystem.isRiftOpen)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/GhostFromAnotherWorld");
            else
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/ApparitionalAccumulation");
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CosmicApparitionHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CosmicApparitionDeath");
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.alpha = 0;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
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

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalMatter>(), minimumDropped: 15, maximumDropped: 45));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<StarmetalBar>(), minimumDropped: 15, maximumDropped: 45));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InterstellarSingularity>(), minimumDropped: 15, maximumDropped: 45));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalStave>(), 4));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ApparitionalDisk>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Starfall>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Vexation>(), 1));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            if (Main.masterMode) {
                NPC.lifeMax = 360000;
                NPC.defense = 75;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 480000;
                NPC.defense = 80;
            }
            else if (DifficultySystem.sinstormMode)
            {
                NPC.lifeMax = 960000;
                NPC.defense = 85;
            }
            else
            {
                NPC.lifeMax = 240000;
                NPC.defense = 70;
            }
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
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            Vector2 targetPosition = Main.player[NPC.target].position;

            var entitySource = NPC.GetSource_FromAI();

            if (NPC.life <= NPC.lifeMax / 2)
            {
                phase = 1;
                attackTimer++;
            }

            #region Flying
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }
            moveTimer++;
            if (moveTimer >= 0)
            {
                Vector2 StartPosition = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float DirectionX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (NPC.velocity.X < DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                    if (NPC.velocity.X < 0 && DirectionX > 0)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                }
                else if (NPC.velocity.X > DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.X > 0 && DirectionX < 0)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                if (NPC.velocity.Y < DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    if (NPC.velocity.Y < 0 && DirectionY > 0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                }
                else if (NPC.velocity.Y > DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (NPC.velocity.Y > 0 && DirectionY < 0)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
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
            NPC.rotation = NPC.velocity.X * 0.06f;
            #endregion

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;
                Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<CosmicSpirit>(), 0, 0f, Main.myPlayer);

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

            player.AddBuff(BuffID.Horrified, 1, false);

            if (attackTimer == 100)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                {
                    int amountOfClones = Main.rand.Next(1, 3);
                    for (int i = 0; i < amountOfClones; ++i)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-30, 30), (int)NPC.Center.Y + Main.rand.Next(-30, 30), ModContent.NPCType<CosmicDecoy>());

                        for (int k = 0; k < 10; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Shadowflame, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                        }

                        if (DifficultySystem.hellMode)
                        {
                            NPC.alpha = 255;
                        }
                    }
                }

            }
            else if (attackTimer == 200 || attackTimer == 225 || attackTimer == 250 || attackTimer == 275)
            {
                int wisps = Main.rand.Next(2, 4);
                for (int i = 0; i < wisps; ++i)
                {
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<CosmicApex>());
                }
            }
            if (attackTimer == 300)
            {
                attackTimer = 0;
            }

            generalAttackTimer++;

            if (generalAttackTimer == 100 || generalAttackTimer == 125 || generalAttackTimer == 150 || generalAttackTimer == 175)
            {
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                int amountOfProjectiles = Main.rand.Next(4, 8);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicPierce>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }
            if (generalAttackTimer == 200 || generalAttackTimer == 205 || generalAttackTimer == 210 || generalAttackTimer == 215)
            {
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                int amountOfProjectiles = Main.rand.Next(4, 8);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 3, 1, Main.myPlayer, 0, 0);
                }
            }
            if (generalAttackTimer == 300 || generalAttackTimer == 350)
            {
                int wisps = Main.rand.Next(2, 4);
                for (int i = 0; i < wisps; ++i)
                {
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-400, 400), (int)NPC.Center.Y + Main.rand.Next(-400, 400), ModContent.NPCType<CosmicApex>());
                }
            }
            else if (generalAttackTimer == 400)
            {
                generalAttackTimer = 0;
            }

            if (phase == 1)
            {
                if (DifficultySystem.hellMode)
                {
                    if (NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                    {
                        NPC.dontTakeDamage = true;
                    }
                    if (!NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                    {
                        NPC.dontTakeDamage = false;
                        while (NPC.alpha > 0)
                        {
                            NPC.alpha -= 15;
                        }
                    }
                }

                if (!phase2Warn)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.position);
                    phase2Warn = true;
                }
            }

            if (teleport)
            {
                teleportTimer++;
            }
            if (teleportTimer == 250)
            {
                SoundEngine.PlaySound(SoundID.DD2_DarkMageCastHeal, NPC.position);
                SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaivePierce, NPC.position);

                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Shadowflame, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                }
                teleportTimer = 0;
                if (phase == 1)
                {
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, -8, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, -8, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, 8, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, 8, ModContent.ProjectileType<ApparitionalWisp>(), NPC.damage / 2, 0, Main.myPlayer, 0f, 0f);
                }
                else
                {
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, -8, ModContent.ProjectileType<ApparitionalDiskHostile>(), NPC.damage / 3, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, -8, ModContent.ProjectileType<ApparitionalDiskHostile>(), NPC.damage / 3, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, 8, ModContent.ProjectileType<ApparitionalDiskHostile>(), NPC.damage / 3, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, 8, ModContent.ProjectileType<ApparitionalDiskHostile>(), NPC.damage / 3, 0, Main.myPlayer, 0f, 0f);
                }
            }

            if (!player.active || player.dead)
            {
                teleport = false;
                NPC.TargetClosest(false);
                NPC.direction = 1;
                NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                if (NPC.timeLeft > 5)
                {
                    NPC.timeLeft = 5;
                    return;
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (!DownedBossSystem.downedCosmicApparition)
            {
                Main.NewText("The cosmic entities have been empowered...", 120, 160, 240);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<CosmicApparitionSoul>(), 0, 0f, Main.myPlayer);
            }

            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, -1f, 0, default(Color), 1f);
            }

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("CosmicApparitionHead").Type;
                int gore2 = Mod.Find<ModGore>("CosmicApparitionBody").Type;
                int gore3 = Mod.Find<ModGore>("CosmicApparitionArm").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
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
            if (phase >= 1)
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
