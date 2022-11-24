using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.NPCs.Town;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperorFly : ModNPC
    {

        int attackTimer;
        int teleportTimer = 0;

        bool canTeleport = false;
        bool tohouAttack = false;

        int tohouTimer = 0;
        int Phase = 0;

        bool phase2Init = false;
        bool phase3Init = false;
        bool phase4Init = false;
        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 60;
            NPC.defense = 90;
            NPC.damage = 40;
            NPC.lifeMax = 40000000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            Music = MusicID.OtherworldlyLunarBoss;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.knockBackResist = -1f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 44000000;
                NPC.defense = 182;
                NPC.damage = 80;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 46000000;
                NPC.defense = 184;
                NPC.damage = 86;
            }
            else
            {
                NPC.lifeMax = 42000000;
                NPC.defense = 180;
                NPC.damage = 60;
            }
        }

        public override void AI()
        {
            if (!justSpawned)
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Emperor>()))
                {
                    Main.NewText("DON'T LET YOUR GUARD DOWN, THIS IS OUR CALL TO ACTION! EVERYONE RELIES ON YOUR HOPE, YOU CAN DO IT!", 24, 96, 210);
                }

                justSpawned = true;
            }

            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            attackTimer++;

            if (!tohouAttack)
            {

                if (DifficultySystem.hellMode)
                {
                    if (attackTimer == 100 || attackTimer == 102 || attackTimer == 104 || attackTimer == 106 || attackTimer == 108 || attackTimer == 110)
                    {

                    }
                    if (attackTimer >= 112)
                    {
                        attackTimer = 0;
                    }
                }
                else
                {
                    if (Phase == 1)
                    {
                        Vector2 targetPosition = Main.player[NPC.target].position;
                        if (canTeleport)
                        {
                            teleportTimer++;
                        }
                        if (teleportTimer >= 100)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                            }
                        }
                        if (teleportTimer >= 200)
                        {
                            SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                            NPC.position.X = targetPosition.X + Main.rand.Next(-600, 600);
                            NPC.position.Y = targetPosition.Y + Main.rand.Next(-600, 600);
                            for (int i = 0; i < 25; i++)
                            {
                                Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                                Dust dust = Dust.NewDustPerfect(NPC.position, DustID.PurpleTorch);
                                dust.noGravity = true;
                                dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                                dust.noLight = false;
                                dust.fadeIn = 1f;
                            }
                            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                            direction.Normalize();
                            direction.X *= 8.5f;
                            direction.Y *= 8.5f;

                            int amountOfProjectiles = 4;
                            for (int i = 0; i < amountOfProjectiles; ++i)
                            {
                                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                                int damage = Main.expertMode ? 110 : 220;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    SoundEngine.PlaySound(SoundID.Item71, NPC.position);
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicPierce>(), damage, 1, Main.myPlayer, 0, 0);
                            }

                            teleportTimer = 0;
                        }
                    }
                    else if (Phase == 2)
                    {

                    }
                    else
                    {
                        if (attackTimer == 100 || attackTimer == 110 || attackTimer == 120 || attackTimer == 130 || attackTimer == 140 || attackTimer == 150)
                        {

                        }
                        if (attackTimer == 160 || attackTimer == 165 || attackTimer == 170 || attackTimer == 175)
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
                                    SoundEngine.PlaySound(SoundID.Item71, NPC.position);
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), damage, 1, Main.myPlayer, 0, 0);
                            }
                        }
                        if (attackTimer == 180 || attackTimer == 185 || attackTimer == 190 || attackTimer == 195)
                        {
                            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                            direction.Normalize();
                            direction.X *= 8.5f;
                            direction.Y *= 8.5f;

                            int amountOfProjectiles = Main.rand.Next(12, 16);
                            for (int i = 0; i < amountOfProjectiles; ++i)
                            {
                                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                                int damage = Main.expertMode ? 110 : 220;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    SoundEngine.PlaySound(SoundID.Item71, NPC.position);
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), damage, 1, Main.myPlayer, 0, 0);
                            }
                        }
                        if (attackTimer >= 200)
                        {
                            attackTimer = 0;
                        }
                    }
                }
                tohouTimer = 0;
            }
            if (tohouAttack)
            {
                tohouTimer++;
                NPC.dontTakeDamage = true;

                if (DifficultySystem.hellMode)
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(4, 16), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(-16, -4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, Main.rand.Next(4, 16), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, Main.rand.Next(-16, -4), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, Main.rand.Next(4, 16), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, Main.rand.Next(-16, -4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(4, 16), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(-16, -4), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                else
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), -4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, 4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, -4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, -4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, 4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, -4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                if (attackTimer >= 121)
                {
                    attackTimer = 0;
                }

                if (tohouTimer == 2000)
                {
                    NPC.dontTakeDamage = false;
                    tohouAttack = false;
                    tohouTimer = 0;
                    if (Phase == 1)
                    {
                        Main.NewText("Impressive, but it's not over yet, " + player.name + "...", 150, 36, 120);
                        canTeleport = true;
                    }
                    if (Phase == 2)
                    {
                        Main.NewText("Now you're really ticking me off! Why can't you just stand still and let me kill you?", 150, 36, 120);
                    }
                }
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                if (!phase2Init)
                {
                    Main.NewText("I'm surprised you're still alive, haven't lost a single limb have you. How about you survive this!", 150, 36, 120);
                    Phase = 1;
                    phase2Init = true;
                    tohouAttack = true;
                }
            }
            if (NPC.life < NPC.lifeMax / 3)
            {
                if (!phase3Init)
                {
                    Main.NewText("Son of a goat, just leave me be already!", 150, 36, 120);
                    Phase = 2;
                    phase3Init = true;
                    tohouAttack = true;
                }
            }
            if (NPC.life < NPC.lifeMax / 4)
            {
                if (!phase4Init)
                {
                    Main.NewText("I can't fall to someone like you! are you even understanding what I am saying here, you deviously mischevious being!", 150, 36, 120);
                    Phase = 2;
                    phase4Init = true;
                }
            }

            #region Death Dialogue
            if (player.dead)
            {
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("Wow, how pathetic!", 150, 36, 120);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("Woops, your finger might have slipped!", 150, 36, 120);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("Ha, I always wondered you were a bit weak...", 150, 36, 120);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText("I sure can't wait to see you suffer more and more, " + player.name + "!", 150, 36, 120);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("I sure hope that this won't get annoying... Especially if you keep on getting yourself into a moment where you just can't make that cut!", 150, 36, 120);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Maybe Calamitis was right with you dying over and over again after all!", 150, 36, 120);
                }
                if (Main.rand.NextBool(7))
                {
                    Main.NewText("Congratulations, you've died! You also have just set a record for dying that easily!", 150, 36, 120);
                }
                if (Main.rand.NextBool(8))
                {
                    Main.NewText("You son of a gun, this dying of your's can stop any moment now!", 150, 36, 120);
                }
                NPC.active = false;
            }
            #endregion
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot, NPC.position);
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("No!", 150, 36, 120);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("I shall not tolerate such action...", 150, 36, 120);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("What is wrong with you?", 150, 36, 120);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText("You think your black magic can withstand my potental?", 150, 36, 120);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("What an absolute cheater you are.", 150, 36, 120);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 150, 36, 120);
                }
                if (Main.rand.NextBool(7))
                {
                    Main.NewText("That did not penetrate me...", 150, 36, 120);
                }
                if (Main.rand.NextBool(8))
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 150, 36, 120);
                }

                damage = 0;
            }
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            float speed;
            if (DifficultySystem.hellMode)
            {
                speed = 48f;
            }
            else
            {
                speed = 36f;
            }
            float acceleration = 0.20f;
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
