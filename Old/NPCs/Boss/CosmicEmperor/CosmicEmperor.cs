using Eternal.Buffs;
using Eternal.Items.Potions;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperor : ModNPC
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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");
            NPCID.Sets.TrailCacheLength[npc.type] = 18;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 50;
            npc.defense = 225;
            npc.damage = 100;
            npc.lifeMax = 9000000;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/EmperorHit");
            npc.DeathSound = null;
            music = MusicID.Boss5;
            npc.buffImmune[BuffID.Chilled] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Suffocation] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.BetsysCurse] = true;
            npc.buffImmune[BuffID.Daybreak] = true;
            npc.buffImmune[BuffID.DryadsWardDebuff] = true;
            npc.buffImmune[ModContent.BuffType<EmbericCombustion>()] = true;
            npc.buffImmune[ModContent.BuffType<DoomFire>()] = true;
            npc.buffImmune[ModContent.BuffType<RedFracture>()] = true;
            if (Eternal.instance.CalamityLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TemporalSadness")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("SilvaStun")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TimeSlow")] = true;
                npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("PearlAura")] = true;
            }
            if (Eternal.instance.FargowiltasModLoaded)
            {
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Lethargic")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("Sadism")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("GodEater")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("ClippedWings")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("MutantNibble")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("OceanicMaul")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("TimeFrozen")] = true;
                npc.buffImmune[ModLoader.GetMod("FargowiltasSouls").BuffType("LightningRod")] = true;
            }
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.knockBackResist = -1f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12000000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 24000000;
            }
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            attackTimer++;

            if (!tohouAttack)
            {

                if (EternalWorld.hellMode)
                {
                    if (attackTimer == 100 || attackTimer == 102 || attackTimer == 104 || attackTimer == 106 || attackTimer == 108 || attackTimer == 110)
                    {
                        if (Main.rand.Next(1) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<StarBomb>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                        if (Main.rand.Next(2) == 0)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<CosmicRing>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                        }

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
                        Vector2 targetPosition = Main.player[npc.target].position;
                        if (canTeleport)
                        {
                            teleportTimer++;
                        }
                        if (teleportTimer >= 100)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.PurpleTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                            }
                        }
                        if (teleportTimer >= 200)
                        {
                            Main.PlaySound(SoundID.Item8, Main.myPlayer);
                            npc.position.X = targetPosition.X + Main.rand.Next(-600, 600);
                            npc.position.Y = targetPosition.Y + Main.rand.Next(-600, 600);
                            for (int i = 0; i < 25; i++)
                            {
                                Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                                Dust dust = Dust.NewDustPerfect(npc.position, DustID.PurpleTorch);
                                dust.noGravity = true;
                                dust.velocity = Vector2.Normalize(position - npc.Center) * 4;
                                dust.noLight = false;
                                dust.fadeIn = 1f;
                            }
                            Vector2 direction = Main.player[npc.target].Center - npc.Center;
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
                                    Main.PlaySound(SoundID.Item71, npc.position);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<EmperorBlade>(), damage, 1, Main.myPlayer, 0, 0);
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
                            if (Main.rand.Next(1) == 0)
                            {
                                Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<StarBomb>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<CosmicRing>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                            }
                        }
                        if (attackTimer == 160 || attackTimer == 165 || attackTimer == 170 || attackTimer == 175)
                        {
                            Vector2 direction = Main.player[npc.target].Center - npc.Center;
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
                                    Main.PlaySound(SoundID.Item71, npc.position);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<EmperorPierce>(), damage, 1, Main.myPlayer, 0, 0);
                            }
                        }
                        if (attackTimer == 180 || attackTimer == 185 || attackTimer == 190 || attackTimer == 195)
                        {
                            Vector2 direction = Main.player[npc.target].Center - npc.Center;
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
                                    Main.PlaySound(SoundID.Item71, npc.position);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), damage, 1, Main.myPlayer, 0, 0);
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
                npc.dontTakeDamage = true;

                if (EternalWorld.hellMode)
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(4, 16), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(-16, -4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, Main.rand.Next(4, 16), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, Main.rand.Next(-16, -4), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, Main.rand.Next(4, 16), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, Main.rand.Next(-16, -4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(4, 16), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(-16, -4), ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                else
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), -4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, 4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, -4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, -4, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, 4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, -4, ModContent.ProjectileType<CosmicEmperorTohou>(), 50, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                if (attackTimer >= 121)
                {
                    attackTimer = 0;
                }

                if (tohouTimer == 2000)
                {
                    npc.dontTakeDamage = false;
                    tohouAttack = false;
                    tohouTimer = 0;
                    if (Phase == 1)
                    {
                        Main.NewText("Impressive, but it's not over yet, " + player.name + "...", 0, 95, 215);
                        canTeleport = true;
                    }
                    if (Phase == 2)
                    {
                        Main.NewText("Now you're really ticking me off! Why can't you just stand still and let me kill you?", 0, 95, 215);
                    }
                }
            }

            if (npc.life < npc.lifeMax / 2)
            {
                if (!phase2Init)
                {
                    Main.NewText("I'm surprised you're still alive, haven't lost a single limb have you. How about you survive this!", 0, 95, 215);
                    Phase = 1;
                    phase2Init = true;
                    tohouAttack = true;
                }
            }
            if (npc.life < npc.lifeMax / 3)
            {
                if (!phase3Init)
                {
                    Main.NewText("Son of a goat, just leave me be already!", 0, 95, 215);
                    Phase = 2;
                    phase3Init = true;
                    tohouAttack = true;
                }
            }
            if (npc.life < npc.lifeMax / 4)
            {
                if (!phase4Init)
                {
                    Main.NewText("I can't fall to someone like you! are you even understanding what I am saying here, you deviously mischevious being!", 0, 95, 215);
                    Phase = 2;
                    phase4Init = true;
                }
            }

            #region Death Dialogue
            if (player.dead)
            {
                if (Main.rand.Next(1) == 0)
                {
                    Main.NewText("Wow, how pathetic!", 0, 95, 215);
                }
                if (Main.rand.Next(2) == 0)
                {
                    Main.NewText("Woops, your finger might have slipped!", 0, 95, 215);
                }
                if (Main.rand.Next(3) == 0)
                {
                    Main.NewText("Ha, I always wondered you were a bit weak...", 0, 95, 215);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.NewText("I sure can't wait to see you suffer more and more, " + player.name + "!", 0, 95, 215);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.NewText("I sure hope that this won't get annoying... Especially if you keep on getting yourself into a moment where you just can't make that cut!", 0, 95, 215);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.NewText("Maybe Calamitis was right with you dying over and over again after all!", 0, 95, 215);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.NewText("Congratulations, you've died! You also have just set a record for dying that easily!", 0, 95, 215);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.NewText("You son of a gun, this dying of your's can stop any moment now!", 0, 95, 215);
                }
                npc.active = false;
            }
            #endregion
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width, npc.height);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Texture2D shadowTexture = mod.GetTexture("NPCs/Boss/CosmicEmperor/CosmicEmperor_Shadow");
                SpriteEffects spriteEffects = npc.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(shadowTexture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
            }
            return true;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.PlaySound(SoundID.DD2_BallistaTowerShot, (int)npc.position.X, (int)npc.position.Y);
                if (Main.rand.Next(1) == 0)
                {
                    Main.NewText("No!", 0, 95, 215);
                }
                if (Main.rand.Next(2) == 0)
                {
                    Main.NewText("I shall not tolerate such action...", 0, 95, 215);
                }
                if (Main.rand.Next(3) == 0)
                {
                    Main.NewText("What is wrong with you?", 0, 95, 215);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.NewText("You think your black magic can withstand my potental?", 0, 95, 215);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.NewText("What an absolute cheater you are.", 0, 95, 215);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.NewText("Don't you butcher me with your nonsense!", 0, 95, 215);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.NewText("That did not penetrate me...", 0, 95, 215);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.NewText("Maybe you should go butcher someone else, not me!", 0, 95, 215);
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
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);

            float speed;
            if (EternalWorld.hellMode)
            {
                speed = 48f;
            }
            else
            {
                speed = 36f;
            }
            float acceleration = 0.20f;
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

            return true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void NPCLoot()
        {
            //Main.NewText("You've done well... here is a gift that you can have!", 0, 95, 215);
            /*Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InterstellarMetal>(), 99);
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CosmoniumFragment>(), 99);

            if (Main.rand.Next(1) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ExosiivaGladiusBlade>());
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheBigOne>());
            }
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Exelodon>());
            }*/
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicEmperorP3>());
        }

    }
}
