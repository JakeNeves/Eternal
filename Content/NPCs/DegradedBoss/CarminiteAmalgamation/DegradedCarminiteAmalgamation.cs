using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Explosion;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DegradedBoss.CarminiteAmalgamation
{
    [AutoloadBossHead]
    public class DegradedCarminiteAmalgamation : ModNPC
    {
        private Player player;

        private float speed;
        public float rot;

        int Timer;
        int DeathTimer;
        int deathExplosionTimer = 0;

        bool isDead = false;
        bool dontKillyet = false;
        bool phase2Init = false;

        public override string Texture => "Eternal/Content/NPCs/Boss/CarminiteAmalgamation/CarminiteAmalgamation";
        public override string HeadTexture => "Eternal/Content/NPCs/Boss/CarminiteAmalgamation/CarminiteAmalgamation_Head_Boss";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 2000;
            NPC.damage = 10;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 80;
            NPC.height = 84;
            NPC.value = Item.buyPrice(gold: 15);
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CarminiteAmalgamationHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CarminiteAmalgamationDeath");
            if (DifficultySystem.hellMode)
                NPC.scale = 0.75f;
            else
                NPC.scale = 1f;
            NPC.rarity = 4;
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
                int gore1 = Mod.Find<ModGore>("CarminiteAmalgamationEye").Type;
                int gore2 = Mod.Find<ModGore>("CarminiteAmalgamationFang1").Type;
                int gore3 = Mod.Find<ModGore>("CarminiteAmalgamationFang2").Type;
                int gore4 = Mod.Find<ModGore>("CarminiteAmalgamationLeftHalf").Type;
                int gore5 = Mod.Find<ModGore>("CarminiteAmalgamationRightHalf").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore4);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore5);
            }

            for (int k = 0; k < 15.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MrFishbone>(), 36));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && DownedBossSystem.downedCarminiteAmalgamation && Main.bloodMoon && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.CarminiteAmalgamation.CarminiteAmalgamation>()))
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
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
            var entitySource = NPC.GetSource_FromAI();

            if (NPC.AnyNPCs(ModContent.NPCType<DegradedCarminiteAmalgamationTenticle>()))
                NPC.dontTakeDamage = true;
            else
                NPC.dontTakeDamage = false;

            rot = NPC.rotation;

            Timer++;
            Target();
            RotateNPCToTarget();
            DespawnHandler();

            if (Timer >= 0)
            {
                Move(new Vector2(0, 0f));
            }
            if (phase2Init)
            {
                if (Timer == 100 || Timer == 110 || Timer == 120 || Timer == 130 || Timer == 140 || Timer == 150 || Timer == 160)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.Center);

                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CarminiteSludge>(), (int)(NPC.damage * 0.25f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CarminiteTooth>(), (int)(NPC.damage * 0.25f), 0f, Main.myPlayer);
                }
                if (Timer == 200)
                {
                    Timer = 0;
                }
            }
            else
            {
                if (Timer == 100 || Timer == 105 || Timer == 110 || Timer == 115 || Timer == 120 || Timer == 125 || Timer == 130)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.Center);

                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CarminiteSludge>(), (int)(NPC.damage * 0.5f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CarminiteTooth>(), (int)(NPC.damage * 0.5f), 0f, Main.myPlayer);
                }
                if (Timer == 250)
                {
                    Timer = 0;
                }
            }
                
            if(NPC.life < NPC.lifeMax / 2)
            {
                if (!phase2Init)
                {
                    if (DifficultySystem.hellMode)
                    {
                        int amountOfTenticles = Main.rand.Next(4, 8);
                        for (int i = 0; i < amountOfTenticles; ++i)
                        {
                            int tenticle = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DegradedCarminiteAmalgamationTenticle>());
                        }
                    }
                    else if (Main.expertMode)
                    {
                        int amountOfTenticles = Main.rand.Next(3, 6);
                        for (int i = 0; i < amountOfTenticles; ++i)
                        {
                            int tenticle = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DegradedCarminiteAmalgamationTenticle>());
                        }
                    }
                    else
                    {
                        int amountOfTenticles = Main.rand.Next(2, 4);
                        for (int i = 0; i < amountOfTenticles; ++i)
                        {
                            int tenticle = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DegradedCarminiteAmalgamationTenticle>());
                        }
                    }
                    phase2Init = true;
                }
            }

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;
                deathExplosionTimer++;

                if (deathExplosionTimer > 5)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<BloodBurst>(), 0, 0f, Main.myPlayer);
                    deathExplosionTimer = 0;
                }

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                if (Main.rand.NextBool(5) && NPC.ai[3] < 180f)
                {
                    /* for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, DustID.Blood, 0f, 0f, 0, default(Color), 1f)];
                        dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width * 1.5f, NPC.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
                    } */
                }

                if (NPC.ai[3] >= 180f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }
        }

        private void Move(Vector2 offset)
        {
            if (DifficultySystem.hellMode)
            {
                if (NPC.lifeMax <= NPC.life / 2)
                    speed = 7.25f;
                else
                    speed = 6f;
            }
            else
            {
                if (NPC.lifeMax <= NPC.life / 2)
                    speed = 6f;
                else
                    speed = 4f;
            }
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        private void Target()
        {
            player = Main.player[NPC.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        public override bool CheckActive()
        {
            Player player = Main.player[NPC.target];
            return !player.active || player.dead;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
