using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Enemy;
using Eternal.Content.Projectiles.Explosion;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    [AutoloadBossHead]
    public class TrinityCore : ModNPC
    {
        public static LocalizedText TrinityDefeated { get; private set; }

        const float Speed = 25f;
        const float Acceleration = 0.15f;
        
        ref float AttackTimer => ref NPC.ai[1];

        bool justSpawned = false;

        int frameNum;
        int deathExplosionTimer = 0;

        bool justSpawnedMindEffigy = false;
        bool justSpawnedBodyEffigy = false;
        bool justSpawnedSoulEffigy = false;

        static int aiTrinityShotRateMax = 24;
        int aiTrinityShotRate = aiTrinityShotRateMax;

        public override void SetStaticDefaults()
        {
            TrinityDefeated = Mod.GetLocalization($"BossDefeatedEvent.{nameof(TrinityDefeated)}");

            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 64;
            NPC.lifeMax = 300000;
            NPC.defense = 80;
            NPC.damage = 70;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/TrinityHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/TrinityDeath");
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TrinitalRetribution");
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            if (!DownedBossSystem.downedTrinity)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(TrinityDefeated.Value, 200, 50, 200);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(TrinityDefeated.ToNetworkText(), new Color(200, 50, 200));
                }
            }

            NPC.SetEventFlagCleared(ref DownedBossSystem.downedTrinity, -1);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PerfectHealingPotion>();
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TrinityBag>()));
        }

        public override bool PreAI()
        {
            var entitySource = NPC.GetSource_FromAI();

            if (NPC.AnyNPCs(ModContent.NPCType<MindEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<BodyEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<SoulEffigy>()))
                NPC.dontTakeDamage = true;
            else
                NPC.dontTakeDamage = false;

            if (NPC.life < NPC.lifeMax / 2f)
            {
                frameNum = 1;

                if (!justSpawnedMindEffigy)
                {
                    Main.NewText("Thunderius, Effigy of the Mind has been reborn!", 175, 75, 255);

                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/TrinitySummon"));
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MindEffigy>(), NPC.whoAmI);

                    justSpawnedMindEffigy = true;
                }
            }
            if (NPC.life < NPC.lifeMax / 2.5f)
            {
                frameNum = 2;

                if (!justSpawnedBodyEffigy)
                {
                    Main.NewText("Infernito, Effigy of the Body has been reborn!", 175, 75, 255);

                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/TrinitySummon"));
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<BodyEffigy>(), NPC.whoAmI);

                    justSpawnedBodyEffigy = true;
                }
            }
            if (NPC.life < NPC.lifeMax / 3f)
            {
                frameNum = 3;

                if (!justSpawnedSoulEffigy)
                {
                    Main.NewText("Cryota, Effigy of the Soul has been reborn!", 175, 75, 255);

                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/TrinitySummon"));
                    NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SoulEffigy>(), NPC.whoAmI);

                    justSpawnedSoulEffigy = true;
                }
            }

            if (!justSpawned)
            {
                Vector2 circDir = new Vector2(0f, 45f);

                for (int i = 0; i < 15; i++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int projectile = Projectile.NewProjectile(entitySource, NPC.Center, circDir, ProjectileID.HallowBossSplitShotCore, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[projectile].hostile = true;
                        Main.projectile[projectile].friendly = false;
                    }
                }

                justSpawned = true;
            }

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            AttackTimer++;
            if (AttackTimer >= 0)
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
            }

            return true;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 0.5f, 1.06f, 2.55f);

            if (!(NPC.AnyNPCs(ModContent.NPCType<MindEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<BodyEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<SoulEffigy>())))
            {
                if (NPC.life < NPC.lifeMax / 2)
                    AI_Trinity_Attacks_Phase2();
                else
                    AI_Trinity_Attacks_Phase1();
            }

            if (NPC.ai[3] > 0f)
            {
                entitySource = NPC.GetSource_Death();

                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;
                deathExplosionTimer++;

                if (deathExplosionTimer > 4)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<TrinityBurst>(), 0, 0);
                    deathExplosionTimer = 0;
                }

                NPC.velocity = new Vector2(0f, 0f);

                if (NPC.ai[3] >= 360f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }
            else
            {
                if (!(NPC.AnyNPCs(ModContent.NPCType<MindEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<BodyEffigy>()) || NPC.AnyNPCs(ModContent.NPCType<SoulEffigy>())))
                {
                    if (Main.rand.NextBool(36))
                    {
                        if (!Main.dedServ)
                            Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), ProjectileID.HallowBossRainbowStreak, NPC.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }

                if (Main.rand.NextBool(48))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.MoonlordTurretLaser, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                    }
                }
            }
        }

        private void AI_Trinity_Attacks_Phase1()
        {
            Vector2 circDir = new Vector2(0f, 45f);

            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (AttackTimer >= 100 && AttackTimer < 200)
            {
                aiTrinityShotRate--;

                if (aiTrinityShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 300 && AttackTimer < 600)
            {
                if (Main.rand.NextBool(6))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(15f, 20f));
                        int j = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CryotasWisp>(), NPC.damage / 4, 1f);
                        Main.projectile[j].hostile = true;
                        Main.projectile[j].tileCollide = true;
                        Main.projectile[j].friendly = false;
                    }
                }
            }

            if (AttackTimer >= 750 && AttackTimer < 800)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                aiTrinityShotRate--;

                if (aiTrinityShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                        SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 90;
                    }

                    aiTrinityShotRate = aiTrinityShotRateMax;
                }
            }

            if (AttackTimer > 900)
            {
                AttackTimer = 0;
            }
        }

        private void AI_Trinity_Attacks_Phase2()
        {
            Vector2 circDir = new Vector2(0f, 45f);

            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (AttackTimer >= 100 && AttackTimer < 250)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                    Main.projectile[proj].timeLeft = 100;
                }
            }

            if (AttackTimer >= 300 && AttackTimer < 325)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                aiTrinityShotRate--;

                if (aiTrinityShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                    }

                    aiTrinityShotRate = aiTrinityShotRateMax;
                }
            }

            if (AttackTimer >= 400 && AttackTimer < 550)
            {
                aiTrinityShotRate--;

                if (aiTrinityShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 0f);

                        Main.projectile[proj].timeLeft = 100;
                    }
                }
            }

            if (AttackTimer > 610)
            {
                AttackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
