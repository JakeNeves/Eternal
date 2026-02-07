using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Tiles.Decorative;
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

namespace Eternal.Content.NPCs.Boss.Incinerius
{
    [AutoloadBossHead]
    public class Incinerius : ModNPC
    {
        ref float AttackTimer => ref NPC.ai[1];

        int fireRate()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 16;
            if (Main.expertMode)
                rate = 24;
            else
                rate = 32;

            return rate;
        }

        int fireTime = 12;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("An infernal construct capable of burning anything to ashes with no warning, it has been said, he was the original subzero elemental before melting and incimatizing himself to the heat of the underworld, never to be seen again!")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 116;
            NPC.boss = true;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/FlamesofResonance");
            NPC.aiStyle = -1;
            NPC.damage = 10;
            NPC.defense = 12;
            NPC.lifeMax = 40000;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath42;
            NPC.npcSlots = 6;
        }

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("IncineriusHead").Type;
                int gore2 = Mod.Find<ModGore>("IncineriusBody").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
            }

            NPC.SetEventFlagCleared(ref DownedBossSystem.downedIncinerius, -1);

            CreateBrickBoxForIncinerius();
        }

        // taken from the vanilla source code!
        private void CreateBrickBoxForIncinerius()
        {
            int num = (int)(NPC.position.X + (float)(NPC.width / 2)) / 16;
            int num2 = (int)(NPC.position.Y + (float)(NPC.height / 2)) / 16;
            int num3 = NPC.width / 2 / 16 + 1;
            for (int i = num - num3; i <= num + num3; i++)
            {
                for (int j = num2 - num3; j <= num2 + num3; j++)
                {
                    Tile tile;
                    if (i == num - num3 || i == num + num3 || j == num2 - num3 || j == num2 + num3)
                    {
                        tile = Main.tile[i, j];
                        if (!tile.HasTile)
                        {
                            tile = Main.tile[i, j];
                            tile.TileType = (ushort)ModContent.TileType<BasaltBrick>();
                            tile = Main.tile[i, j];
                            tile.HasTile = true;
                        }
                    }
                    tile = Main.tile[i, j];
                    tile.LiquidType = LiquidID.Lava;
                    tile = Main.tile[i, j];
                    tile.LiquidAmount = 0;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, i, j);
                    else
                        WorldGen.SquareTileFrame(i, j);
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (Main.rand.NextBool(2))
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, 0, 0, default(Color), 1f);
        }

        public override bool PreAI()
        {
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.02f;

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
                NPC.velocity.Y -= 0.15f;
                NPC.EncourageDespawn(10);
            }

            NPC.rotation = NPC.velocity.X * 0.01f;

            float speed = 16f;
            float acceleration = 0.06f;
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

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.02f;
                NPC.EncourageDespawn(10);
                return;
            }

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (NPC.life < NPC.lifeMax / 2)
                AI_Incinerius_Attacks_Phase2();
            else
                AI_Incinerius_Attacks_Phase1();
        }

        private void AI_Incinerius_Attacks_Phase1()
        {
            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;

            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (AttackTimer > 200 && AttackTimer < 300 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.BallofFire, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Main.projectile[index5].friendly = false;
                Main.projectile[index5].hostile = true;
                Main.projectile[index5].tileCollide = false;
                Main.projectile[index5].timeLeft = 100;
            }

            if (AttackTimer > 450 && AttackTimer < 500 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (--fireTime <= 0)
                {
                    fireTime = fireRate();

                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-2f, 2f), -8f, ProjectileID.CultistBossFireBall, NPC.damage / 4, 0f);
                    Main.projectile[proj].extraUpdates = 3;
                    Main.projectile[proj].timeLeft = 200;
                }
            }

            if (AttackTimer > 500 && AttackTimer < 650)
            {
                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, -2.5f, 0, default, 1.7f);

                NPC.velocity = new Vector2(0f, 0f);
            }

            if (AttackTimer == 650 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);
            }

            if (AttackTimer >= 800 && AttackTimer <= 1000)
            {
                if (--fireTime <= 0)
                {
                    fireTime = fireRate();

                    int amountOfProjectiles = 2 + Main.rand.Next(4);
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                        if (Main.netMode != NetmodeID.Server)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                        if (Main.netMode != NetmodeID.Server)
                        {
                            int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DD2BetsyFireball, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                            Main.projectile[proj].extraUpdates = 2;
                            Main.projectile[proj].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer > 1100)
            {
                AttackTimer = 0;
            }
        }

        private void AI_Incinerius_Attacks_Phase2()
        {
            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (AttackTimer >= 400 && AttackTimer < 600)
            {
                if (DifficultySystem.hellMode)
                {
                    if (Main.rand.NextBool(2))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(20f, 30f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].tileCollide = false;
                        Main.projectile[i].timeLeft = 100;
                    }
                }
                else if (Main.expertMode)
                {
                    if (Main.rand.NextBool(4))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(20f, 30f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].tileCollide = false;
                        Main.projectile[i].timeLeft = 100;
                    }
                }
                else
                {
                    if (Main.rand.NextBool(6))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(20f, 30f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 800 && AttackTimer <= 1200)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (AttackTimer > 900 && AttackTimer < 1200)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (--fireTime <= 0)
                        {
                            fireTime = fireRate();

                            int amountOfProjectiles = 2 + Main.rand.Next(4);
                            for (int i = 0; i < amountOfProjectiles; ++i)
                            {
                                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                                float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                                if (Main.netMode != NetmodeID.Server)
                                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                                int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DD2BetsyFireball, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                                Main.projectile[proj].extraUpdates = 2;
                                Main.projectile[proj].timeLeft = 100;
                            }
                        }
                    }
                }
            }

            if (AttackTimer >= 1300 && AttackTimer <= 1500)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (--fireTime <= 0)
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                        if (!Main.dedServ)
                        {
                            int[] i =
                            [
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 4f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage, 0),
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -4f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage, 0),
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 4f, ProjectileID.InfernoHostileBolt, NPC.damage, 0),
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -4f, ProjectileID.InfernoHostileBolt, NPC.damage, 0)
                            ];

                            for (int j = 0; j < i.Length; j++)
                            {
                                Main.projectile[i[j]].extraUpdates = 2;
                                Main.projectile[i[j]].timeLeft = 150;
                            }
                        }

                        fireTime = fireRate();
                    }
                }
            }

            if (AttackTimer >= 1600 && AttackTimer < 1800)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (DifficultySystem.hellMode)
                {
                    if (Main.rand.NextBool(2))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-30f, -20f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.BallofFire, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].friendly = false;
                        Main.projectile[i].hostile = true;
                        Main.projectile[i].timeLeft = 150;
                        Main.projectile[i].extraUpdates = 2;
                    }
                }
                else if (Main.expertMode)
                {
                    if (Main.rand.NextBool(4))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-30f, -20f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.BallofFire, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].friendly = false;
                        Main.projectile[i].hostile = true;
                        Main.projectile[i].tileCollide = false;
                        Main.projectile[i].timeLeft = 150;
                        Main.projectile[i].extraUpdates = 2;
                    }
                }
                else
                {
                    if (Main.rand.NextBool(6))
                    {
                        if (!Main.dedServ)
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-30f, -20f));
                        int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.BallofFire, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                        Main.projectile[i].friendly = false;
                        Main.projectile[i].hostile = true;
                        Main.projectile[i].tileCollide = false;
                        Main.projectile[i].timeLeft = 150;
                        Main.projectile[i].extraUpdates = 2;
                    }
                }
            }

            if (AttackTimer > 2000)
            {
                AttackTimer = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<IncineriusBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IgneoforgedEdge>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Incinerator>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfernalDuplex>(), 4));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CanadianResistance>(), 12));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MagmaticAlloy>(), minimumDropped: 6, maximumDropped: 8));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfernalAshes>(), minimumDropped: 12, maximumDropped: 16));
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
