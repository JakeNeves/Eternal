using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Potions;
using Eternal.Projectiles.Boss;
using Eternal.Items.Materials;

namespace Eternal.NPCs.Boss.BionicBosses
{
    [AutoloadBossHead]
    public class Atlas : ModNPC
    {
        private Player player;

        private float speed;

        int Timer;

        bool justSpawnedArmModules = false;
        bool doAtlasAttack = false;
        bool claws = false;
        bool phase2Init = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XR-2006 Atlas-X9");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 620000;
            npc.damage = 60;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 116;
            npc.height = 150;
            npc.value = Item.buyPrice(platinum: 3, gold: 20);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = MusicID.Boss1;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            //bossBag = ItemType<CarmaniteScouterBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1240000;
            npc.damage = 120;
            npc.defense = 60;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 2530000;
                npc.damage = 240;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "SYSTEM FAILIURES DETECTED, SELF-DESTRUCT INITIATED...", dramatic: true);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasHead"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasJaw"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasBody"), 1f);
            }
            else
            {

                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Electric, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Fire, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 1.90f, 0.22f, 0.22f);

            npc.netUpdate = true;
            player = Main.player[npc.target];
            if (!NPC.AnyNPCs(ModContent.NPCType<AtlasCannon>()) && !NPC.AnyNPCs(ModContent.NPCType<AtlasLaser>()) && !NPC.AnyNPCs(ModContent.NPCType<AtlasSaw>()) && !NPC.AnyNPCs(ModContent.NPCType<AtlasSlicers>()) && !NPC.AnyNPCs(ModContent.NPCType<AtlasVice>()))
            {
                npc.dontTakeDamage = false;
                if (npc.ai[0] == 0)
                {
                    float speed = 20.05f;
                    float acceleration = 0.4f;
                    Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                    float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
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
                    if (npc.velocity.X < xDir)
                    {
                        npc.velocity.X = npc.velocity.X + acceleration;
                        if (npc.velocity.X < 0 && xDir > 0)
                            npc.velocity.X = npc.velocity.X + acceleration;
                    }
                    else if (npc.velocity.X > xDir)
                    {
                        npc.velocity.X = npc.velocity.X - acceleration;
                        if (npc.velocity.X > 0 && xDir < 0)
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                    if (npc.velocity.Y < yDir)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                        if (npc.velocity.Y < 0 && yDir > 0)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                    }
                    else if (npc.velocity.Y > yDir)
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                        if (npc.velocity.Y > 0 && yDir < 0)
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                    doAtlasAttack = true;
                }
            }
            else
            {
                Move(new Vector2(0f, -370f));
                npc.dontTakeDamage = true;
                doAtlasAttack = false;
            }
            DespawnHandler();

            return true;
        }

        public override void AI()
        {
            if (!justSpawnedArmModules)
            {
                if (npc.life < npc.lifeMax / 2)
                {
                    NPC.NewNPC((int)npc.position.X - 200, (int)npc.position.Y - 100, ModContent.NPCType<AtlasSaw>());
                    NPC.NewNPC((int)npc.position.X + 200, (int)npc.position.Y - 100, ModContent.NPCType<AtlasSlicers>());
                    NPC.NewNPC((int)npc.position.X - 100, (int)npc.position.Y, ModContent.NPCType<AtlasVice>());
                    NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, ModContent.NPCType<AtlasVice>());
                }
                else
                {
                    NPC.NewNPC((int)npc.position.X - 200, (int)npc.position.Y, ModContent.NPCType<AtlasCannon>());
                    NPC.NewNPC((int)npc.position.X + 200, (int)npc.position.Y, ModContent.NPCType<AtlasLaser>());
                }
                justSpawnedArmModules = true;
            }

            if (doAtlasAttack)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;
                int amountOfProjectiles;

                Timer++;
                if (npc.life < npc.lifeMax / 2)
                {
                    if (!phase2Init)
                    {
                        CombatText.NewText(npc.Hitbox, Color.Red, "DEPLOYING MELEE MODULES...", dramatic: true);
                        justSpawnedArmModules = false;
                        phase2Init = true;
                    }

                    if (Timer == 100 || Timer == 105 || Timer == 110 || Timer == 115 || Timer == 120 || Timer == 125 || Timer == 130 || Timer == 135 || Timer == 140 || Timer == 145 || Timer == 150)
                    {
                        amountOfProjectiles = 1;
                        for (int i = 0; i < amountOfProjectiles; ++i)
                        {
                            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                            float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                            int damage = Main.expertMode ? 125 : 150;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Main.PlaySound(SoundID.DD2_KoboldExplosion, npc.position);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<AtlasMissile>(), damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    if (Timer == 160)
                    {
                        Main.PlaySound(SoundID.Item11, npc.position);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 12, 0, ModContent.ProjectileType<AtlasSpike>(), 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -4, 4, ModContent.ProjectileType<AtlasSpike>(), 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 4, -4, ModContent.ProjectileType<AtlasSpike>(), 6, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                else
                {
                    if (Timer == 100 || Timer == 110 || Timer == 120 || Timer == 130 || Timer == 140 || Timer == 150)
                    {
                        amountOfProjectiles = 4;
                        for (int i = 0; i < amountOfProjectiles; ++i)
                        {
                            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                            float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                            int damage = Main.expertMode ? 100 : 102;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<AtlasSpike>(), damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                }
                if (Timer == 180)
                {
                    Timer = 0;
                }
            }

            /*if (!claws && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                claws = true;
                NPC aClaw = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width * 2)), (int)npc.position.Y + npc.height * 2, ModContent.NPCType<AtlasClaw>(), 0, 0f, 0f, 0f, 0f, 255)];
                aClaw.ai[0] = 1f;
                aClaw.ai[1] = (float)npc.whoAmI;
                aClaw.target = npc.target;
                aClaw.netUpdate = true;
                aClaw = Main.npc[NPC.NewNPC((int)(npc.position.X + (float)(npc.width * 2)), (int)npc.position.Y + npc.height * 2, ModContent.NPCType<AtlasClaw>(), 0, 0f, 0f, 0f, 0f, 255)];
                aClaw.ai[0] = -1f;
                aClaw.ai[1] = (float)npc.whoAmI;
                aClaw.target = npc.target;
                aClaw.netUpdate = true;
            }*/
        }

        private void Move(Vector2 offset)
        {
            speed = 40.5f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrightCore>(), Main.rand.Next(20, 40));

            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WeatheredPlating>(), Main.rand.Next(3, 9));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
