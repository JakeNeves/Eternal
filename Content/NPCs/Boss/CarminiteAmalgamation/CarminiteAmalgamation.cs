﻿using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Items.BossBags;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using Eternal.Content.Tiles;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Projectiles.Explosion;

namespace Eternal.Content.NPCs.Boss.CarminiteAmalgamation
{
    [AutoloadBossHead]
    public class CarminiteAmalgamation : ModNPC
    {

        private Player player;

        private float speed;
        public float rot;

        int Timer;
        int DeathTimer;

        bool isDead = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 3000;
            NPC.damage = 16;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 80;
            NPC.height = 84;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath12;
            Music = MusicID.Boss5;
            if (DifficultySystem.hellMode)
                NPC.scale = 0.75f;
            else
                NPC.scale = 1f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> { 
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A rather failed and accidental experiment from combining blood, flesh and comet samples. This crimson-like amalgamated creature is contained at all costs!")
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                var entitySource = NPC.GetSource_Death();

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
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, hitDirection, 0, 0, default(Color), 1f);
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 9000;
                NPC.damage = 20;
                NPC.defense = 14;

            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 12000;
                NPC.damage = 22;
                NPC.defense = 16;
            }
            else
            {
                NPC.lifeMax = 6000;
                NPC.damage = 18;
                NPC.defense = 12;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CarminiteAmalgamationBag>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Carminite>(), minimumDropped: 12, maximumDropped: 18));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CarminiteBane>(), 1));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CarminitePurgatory>(), 2));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CarminiteRipperClaws>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CarminiteDeadshot>(), 4));

            if (!DownedBossSystem.downedCarminiteAmalgamation)
            {
                Main.NewText("The ground has been smothered with luminous energy...", 22, 71, 73);

                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 6), WorldGen.genRand.Next(4, 12), ModContent.TileType<IesniumOre>());
                }

                DownedBossSystem.downedCarminiteAmalgamation = true;
            }
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            rot = NPC.rotation;

            Timer++;
            Target();
            RotateNPCToTarget();
            DespawnHandler();

            if (Timer >= 0)
            {
                Move(new Vector2(0, 0f));
            }
            if (Timer == 100 || Timer == 105 || Timer == 110 || Timer == 115 || Timer == 120 || Timer == 125 || Timer == 130)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.Center);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CarminiteSludge>(), NPC.damage, 0f, Main.myPlayer);
            }
            if (Timer == 200)
            {
                Timer = 0;
            }

            if (NPC.life < 0)
            {
                isDead = true;
                DeathTimer++;
                if (DeathTimer == 5 || DeathTimer == 10 || DeathTimer == 15 || DeathTimer == 20 || DeathTimer == 25 || DeathTimer == 30 || DeathTimer == 35 || DeathTimer == 40 || DeathTimer == 45)
                {
                    NPC.life = 1;
                    NPC.dontTakeDamage = true;
                    SoundEngine.PlaySound(SoundID.NPCHit1, NPC.Center);
                    Projectile.NewProjectile(entitySource, NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), 0, 0, ModContent.ProjectileType<BloodBurst>(), NPC.damage, 0f, Main.myPlayer);
                }
                if (DeathTimer >= 50)
                {
                    NPC.dontTakeDamage = false;
                    player.ApplyDamageToNPC(NPC, 9999, 0, 0, false);
                }
            }
        }

        private void Move(Vector2 offset)
        {
            if (isDead)
                speed = 0f;
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
            return !player.active || player.dead || Main.dayTime;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}