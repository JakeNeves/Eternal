using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.AoI
{
    [AutoloadBossHead]
    public class ArkofImperious : ModNPC
    {

        bool isDashing = false;
        bool phase2Init = false;
        bool phase3Init = false;
        bool justSpawnedCircle = false;
        bool isDead = false;
        bool dontKillyet = false;

        float speed = 16;
        float acceleration = 0.2f;

        int Phase = 0;
        int AttackTimer = 0;
        int moveTimer;
        int DeathTimer;

        private Player player;

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public static int ShieldCount()
        {
            int count = 8;

            if (Main.expertMode)
            {
                count += 8;
            }
            else if (DifficultySystem.hellMode)
            {
                if (ModContent.GetInstance<ServerConfig>().BrutalHellMode)
                {
                    count += 10;
                }
                else
                {
                    count += 12;
                }
            }

            return count;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious");
            NPCID.Sets.TrailCacheLength[NPC.type] = 12;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.width = 170;
            NPC.height = 418;
            NPC.lifeMax = 2400000;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/AoIHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/AoIDeath");
            NPC.boss = true;
            Music = MusicID.Boss3;
            NPC.defense = 80;
            NPC.damage = 40;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.alpha = 0;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 4800000;
                NPC.damage = 44;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 9600000;
                NPC.damage = 46;
            }
            else
            {
                NPC.lifeMax = 3600000;
                NPC.damage = 42;
            }
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedArkofImperious, -1);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("The god of living swords, anything who dares to oppose this mighty deity will be impaled by it's powerful godly penetration, it's tough and sharp blade is extremely durable against even the strongest metal yet...")
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = TextureAssets.Projectile[NPC.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }
            return true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }

            if (NPC.life <= 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.GreenTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.position);
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                if (!DownedBossSystem.downedArkofImperious)
                {
                    Main.NewText("The seal of the Cosmic Tablet has broken, the stars above are calling upon you...", 100, 24, 60);
                    DownedBossSystem.downedArkofImperious = true;
                }
            }
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenTorch, hitDirection, 0f, 0, default(Color), 0.7f);
                }
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Bleeding, 180, false);
            player.AddBuff(BuffID.BrokenArmor, 180, false);
            if (DifficultySystem.hellMode && ModContent.GetInstance<ServerConfig>().BrutalHellMode)
            {
                player.AddBuff(BuffID.Cursed, 180, false);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PerfectHealingPotion>();
        }

        public override bool PreAI()
        {
            Movement();

            return true;
        }

        private void SpawnShield()
        {
            if (SpawnedShield)
            {
                return;
            }

            SpawnedShield = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int count = ShieldCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ArklingOrbit>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is ArklingOrbit minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;
                }

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        private void Movement()
        {
            float speed = 48f;
            float acceleration = 0.20f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            if (isDashing)
            {
                speed = 48f;
                acceleration = 0.20f;
                NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                speed = 12f;
                acceleration = 0.10f;
                if (DifficultySystem.hellMode)
                {
                    NPC.rotation = NPC.velocity.X * 0.06f;
                }
                else
                {
                    NPC.rotation = NPC.velocity.X * 0.03f;
                }

            }

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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AoIBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), minimumDropped: 6, maximumDropped: 8));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RawArkaniumDebris>(), minimumDropped: 10, maximumDropped: 20));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RawArkrystal>(), minimumDropped: 10, maximumDropped: 20));
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player target = Main.player[NPC.target];

            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            if (!isDead)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    Phase = 1;
                    if (!phase2Init)
                    {
                        AttackTimer = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                           Dust dust = Dust.NewDustPerfect(NPC.position, DustID.GreenTorch);
                           dust.noGravity = true;
                           dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                           dust.noLight = false;
                            dust.fadeIn = 1f;
                        }
                         SoundEngine.PlaySound(SoundID.DD2_SkeletonSummoned, NPC.position);
                         SpawnShield();
                         phase2Init = true;
                    }
                }
                if (NPC.life < NPC.lifeMax / 3)
                {
                    Phase = 2;
                    if (!phase3Init)
                    {
                        AttackTimer = 0;
                        for (int i = 0; i < 25; i++)
                        {
                            Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                            Dust dust = Dust.NewDustPerfect(NPC.position, DustID.JungleTorch);
                            dust.noGravity = true;
                            dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                            dust.noLight = false;
                            dust.fadeIn = 1f;
                        }
                        phase3Init = true;
                    }
                }

            
                AttackTimer++;

                if (Phase == 1)
                {
                    if (!justSpawnedCircle)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AoICircle>(), NPC.damage, 0, 0, 0f, NPC.whoAmI);
                        justSpawnedCircle = true;
                    }
                }
                DoAttacks();

                if (Phase == 2)
                {
                    int maxDist;

                    if (Main.expertMode)
                    {
                        maxDist = 1000;
                    }
                    else if (DifficultySystem.hellMode)
                    {
                        maxDist = 900;
                    }
                    else
                    {
                        maxDist = 2000;
                    }

                    // ripped from another mod, credit to the person who wrote this
                    for (int i = 0; i < 120; i++)
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                        Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset, 0, 0, DustID.GreenTorch, 0, 0, 100)];
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
                        float dustDistance = 200;
                        float dustSpeed = 8;
                        Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                        Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                        Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.GreenTorch, velocity, 0, default(Color), 1.5f);
                        vortex.noGravity = true;
                    }
                }
            }
            else
            {
                DeathTimer++;
                if (DeathTimer > 1)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                    NPC.life = 1;
                    NPC.dontTakeDamage = true;
                    NPC.alpha++;
                }
                if (DeathTimer >= 100)
                {
                    NPC.dontTakeDamage = false;
                    dontKillyet = true;
                    NPC.alpha = 0;
                    target.ApplyDamageToNPC(NPC, 9999, 0, 0, false);

                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                        Dust dust = Dust.NewDustPerfect(NPC.position, DustID.GreenTorch);
                        dust.velocity = Vector2.Normalize(position - NPC.position);
                        dust.noLight = false;
                        dust.fadeIn = 1f;
                    }

                    if (!DownedBossSystem.downedArkofImperious)
                    {
                        DownedBossSystem.downedArkofImperious = true;
                    }
                }
            }

            if (!target.active || target.dead)
            {
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

        private void DoAttacks()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            if (NPC.AnyNPCs(ModContent.NPCType<ArklingOrbit>()))
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                isDashing = false;

                if ((AttackTimer == 120 || AttackTimer == 145 || AttackTimer == 160))
                {
                    if (DifficultySystem.hellMode)
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            NPC.NewNPC(entitySource, (int)NPC.Center.X - 20, (int)NPC.Center.Y, ModContent.NPCType<Arkling>());
                        }
                    }
                    else
                    {

                    }
                }
                if ((AttackTimer == 185 || AttackTimer == 200 || AttackTimer == 225))
                {
                    Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 2;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.position);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                else if (AttackTimer == 250)
                {
                    AttackTimer = 0;
                }
            }
            if (NPC.life < NPC.lifeMax / 3)
            {
                if (AttackTimer == 50 || AttackTimer == 65 || AttackTimer == 70 || AttackTimer == 85 || AttackTimer == 90 || AttackTimer == 105 || AttackTimer == 115 || AttackTimer == 130)
                {
                    Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 2;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 110 : 220;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<ArkArrowHostile>(), damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if ((AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125))
                {
                    Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 4;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.position);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                else if (AttackTimer == 250)
                {
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, -12, 0, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 12, 0, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 0, 12, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 0, -12, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);

                    AttackTimer = 0;
                }
            }
            else
            {
                if ((AttackTimer == 100 || AttackTimer == 150 || AttackTimer == 175))
                {
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, -12, 0, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 12, 0, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 0, 12, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 80, NPC.position.Y + 80, 0, -12, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, -8, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, -8, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, -8, 8, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(entitySource, NPC.position.X + 40, NPC.position.Y + 40, 8, 8, ModContent.ProjectileType<ArkArrowHostile>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);

                    Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 1;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.position);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<ArkEnergyHostile>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (AttackTimer == 300)
                {
                    isDashing = true;
                }
                else if (AttackTimer == 475)
                {
                    isDashing = false;
                    AttackTimer = 0;
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}
