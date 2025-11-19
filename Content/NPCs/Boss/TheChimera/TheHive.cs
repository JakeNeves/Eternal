using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Items.Weapons.Summon;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Enemy;
using Eternal.Content.Projectiles.Explosion;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.TheChimera
{
    [AutoloadBossHead]
    public class TheHive : ModNPC
    {
        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        ref float AttackTimer => ref NPC.ai[1];

        float aiHiveProjectileRotation = MathHelper.PiOver2;

        int deathExplosionTimer = 0;

        int tomaBallFireRate = 12;
        int tomaBallFireTime = 24;

        int aiHiveShootTime = 8;
        int aiHiveShootRate = 16;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 30000;
            NPC.damage = 15;
            NPC.defense = 40;
            NPC.knockBackResist = 0f;
            NPC.width = 80;
            NPC.height = 84;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit13;
            NPC.DeathSound = SoundID.NPCDeath12;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/AmalgamatedHorror");
            NPC.npcSlots = 6;
        }

        public override void OnKill()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<TheHeap>()))
                NPC.SetEventFlagCleared(ref DownedBossSystem.downedChimera, -1);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 15.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
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

            NPC.rotation = NPC.velocity.X * 0.02f;

            float speed = 4f;
            float acceleration = 0.025f;
            if (Main.expertMode)
            {
                speed = 8f;
                acceleration = 0.05f;
            }
            else if (DifficultySystem.hellMode)
            {
                speed = 12f;
                acceleration = 0.075f;
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
            Player player = Main.player[NPC.target];

            NPC.dontTakeDamage = !ModContent.GetInstance<ZoneSystem>().zoneCarrion;

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;

                deathExplosionTimer++;

                if (deathExplosionTimer > 5)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<PussBurst>(), 0, 0f, Main.myPlayer);
                    deathExplosionTimer = 0;
                }

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                if (Main.rand.NextBool(5) && NPC.ai[3] < 180f)
                {

                }

                if (NPC.ai[3] >= 180f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }
            else
            {
                if (NPC.life <= NPC.lifeMax / 2)
                    AI_Hive_Attacks_Phase2();
                else
                    AI_Hive_Attacks_Phase1();
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

        private void AI_Hive_Attacks_Phase1()
        {
            var entitySource = NPC.GetSource_FromAI();

            AttackTimer++;

            if (AttackTimer >= 200 && AttackTimer < 400)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiHiveProjectileRotation += 0.01f;
                    if (--aiHiveShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.Center);

                        aiHiveShootTime = aiHiveShootRate;
                        var shootPos = NPC.Center + new Vector2(0, 15);
                        var shootVel = new Vector2(0, 5).RotatedBy(aiHiveProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<BallofPussHostile>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<BallofPussHostile>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType <BallofPussHostile>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType <BallofPussHostile>(), NPC.damage / 2, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                        }
                    }
                }
            }

            if (AttackTimer >= 600 && AttackTimer < 800)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ModContent.ProjectileType<BloodClot>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].tileCollide = false;
                    Main.projectile[index5].timeLeft = 100;
                }
            }

            if (AttackTimer >= 1000 && AttackTimer <= 1100)
            {
                if (--tomaBallFireTime <= 0)
                {
                    tomaBallFireTime = tomaBallFireRate;

                    if (Main.rand.NextBool(2))
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<TheFammish>());
                    else
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<TomaBall>());
                }
            }

            if (AttackTimer > 1200)
                AttackTimer = 0;
        }

        private void AI_Hive_Attacks_Phase2()
        {
            var entitySource = NPC.GetSource_FromAI();

            AttackTimer++;

            if (AttackTimer >= 150 && AttackTimer <= 200)
            {
                if (--tomaBallFireTime <= 0)
                {
                    tomaBallFireTime = tomaBallFireRate;

                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<TomaBall>());
                }
            }

            if (AttackTimer >= 300 && AttackTimer < 550)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiHiveProjectileRotation += 0.01f;
                    if (--aiHiveShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.Center);

                        aiHiveShootTime = aiHiveShootRate;
                        var shootPos = NPC.Center + new Vector2(0, 15);
                        var shootVel = new Vector2(0, 5).RotatedBy(aiHiveProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<BloodClot>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<BloodClot>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType <BloodClot>(), NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType <BloodClot>(), NPC.damage / 2, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                        }
                    }
                }
            }

            if (AttackTimer >= 600 && AttackTimer <= 800)
            {
                if (--tomaBallFireTime <= 0)
                {
                    tomaBallFireTime = tomaBallFireRate;

                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<TheFammish>());
                }
            }

            if (AttackTimer >= 900 && AttackTimer <= 1000 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<GasCloud>(), (int)(NPC.damage * 0.25f), 0f);
            }

            if (AttackTimer > 1200)
                AttackTimer = 0;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            ChimeraDropCondition chimeraDropCondition = new ChimeraDropCondition();
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            npcLoot.Add(ItemDropRule.BossBagByCondition(chimeraDropCondition, ModContent.ItemType<TheChimeraBag>()));

            notExpertRule.OnSuccess(ItemDropRule.ByCondition(chimeraDropCondition, ModContent.ItemType<RottenFangspear>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(chimeraDropCondition, ModContent.ItemType<RottenMeathook>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(chimeraDropCondition, ModContent.ItemType<Necrobow>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(chimeraDropCondition, ModContent.ItemType<PustuleWand>(), 4));
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(chimeraDropCondition, ModContent.ItemType<RottenStave>(), 5));
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
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
