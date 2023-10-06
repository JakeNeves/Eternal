using Eternal.Common.Configurations;
using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Miniboss
{
    [AutoloadBossHead]
    public class PhantomConstruct : ModNPC
    {
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 320000;
            NPC.damage = 120;
            NPC.defense = 45;
            NPC.knockBackResist = -1f;
            NPC.width = 60;
            NPC.height = 68;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/PhantomConstructHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/PhantomConstructDeath");
            NPC.value = Item.sellPrice(gold: 30);
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.rarity = 4;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

                new FlavorTextBestiaryInfoElement("Appearing from an Unstable Portal, these constructs are made from pure Ominite and renforced with Cometite.")
            });
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (RiftSystem.isRiftOpen && Main.zenithWorld)
            {
                return SpawnCondition.Sky.Chance * 0.5f;
            }
            else
            {
                return SpawnCondition.Sky.Chance * 0f;
            }
        }

        public override void AI()
        {
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                {
                    EternalBossBarOverlay.SetTracked("", NPC, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                    EternalBossBarOverlay.visible = true;
                }
            }

            var entitySource = NPC.GetSource_FromAI();

            Player target = Main.player[NPC.target];
            
            NPC.spriteDirection = NPC.direction = NPC.Center.X < target.Center.X ? -1 : 1;

            NPC.rotation = NPC.velocity.X * 0.03f;

            Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);

            if (NPC.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.zenithWorld)
                {
                    for (int k = 0; k < 25; k++)
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);

                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 60;
                        Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.PinkTorch);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                        dust.noLight = false;
                        dust.fadeIn = 1.25f;
                    }

                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/PhantomConstructSpawn"), NPC.position);
                    Main.NewText("A Phantom Construct has appeared!", 175, 75, 255);
                }

                NPC.TargetClosest(true);
                NPC.ai[0] = 1f;
            }

            attackTimer++;
            Attack();

            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f;
                NPC.dontTakeDamage = true;

                NPC.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                if (Main.rand.NextBool(5) && NPC.ai[3] < 120f)
                {
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, ModContent.DustType<CosmicSpirit>(), 0f, 0f, 0, default(Color), 1f)];
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
                    if (!DownedMinibossSystem.downedPhantomConstruct)
                        DownedMinibossSystem.downedPhantomConstruct = true;
                    if (!Main.zenithWorld)
                        Main.NewText("The Phantom Construct has been defeated!", 175, 75, 255);
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }

                return;
            }

            float speed = 30f;

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
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (attackTimer == 200)
            {
                for (int i = 0; i < Main.rand.Next(2, 4); i++)
                {
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-10, 10), (int)NPC.Center.Y + Main.rand.Next(-10, 10), ModContent.NPCType<DetonatingWisp>());
                }
            }
            if (attackTimer == 250)
            {
                for (int i = 0; i < Main.rand.Next(4, 8); i++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                }
            }
            if (attackTimer == 400)
            {
                if (NPC.ai[3] > 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    NPC.position.X = targetPosition.X + Main.rand.Next(-600, 600);
                    NPC.position.Y = targetPosition.Y + Main.rand.Next(-600, 600);
                }
                attackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life < 0)
            {
                var entitySource = NPC.GetSource_Death();

                int gore1 = Mod.Find<ModGore>("PhantomConstructHead").Type;
                int gore2 = Mod.Find<ModGore>("PhantomConstructBody").Type;
                int gore3 = Mod.Find<ModGore>("PhantomConstructArm").Type;

                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostArkofImperiousRiftDropCondition postAoIRiftDrop = new PostArkofImperiousRiftDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postAoIRiftDrop, ModContent.ItemType<RawOminaquadite>(), 2, 6, 12));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AspectofTheShiftedWarlock>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiftedBlade>(), 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Shiftstorm>(), 3));
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
