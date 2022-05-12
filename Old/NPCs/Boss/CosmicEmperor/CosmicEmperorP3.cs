using Eternal.Buffs;
using Eternal.Items.BossBags;
using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.Items.Tools;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperorP3 : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Emperor");
            Main.npcFrameCount[npc.type] = 4;
        }

        bool bossIntro = true;
        int introTimer = 0;

        int attackTimer = 0;

        bool tohouAttack = false;
        int tohouTimer = 0;
        int Phase = 0;

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 50;
            npc.defense = 300;
            npc.damage = 110;
            npc.lifeMax = 12000000;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/EmperorHit");
            npc.DeathSound = null;
            music = MusicID.LunarBoss;
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
            bossBag = ModContent.ItemType<CosmicEmperorCapsule>();
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
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

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 24000000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 36000000;
            }
        }

        int amountOfProjectiles;

        public override void AI()
        {
            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            Lighting.AddLight(npc.position, 2.55f, 2.55f, 2.55f);

            Player player = Main.player[npc.target];

            if (bossIntro)
            {
                introTimer++;
                npc.dontTakeDamage = true;
                if (npc.life < npc.lifeMax / 3)
                {
                    if (EternalWorld.downedCosmicEmperor)
                    {
                        npc.velocity.Y += 0.01f;
                        npc.noTileCollide = false;

                        switch (introTimer)
                        {
                            case 0:
                                Main.NewText("HALT!", 0, 95, 215);
                                break;
                            case 120:
                                Main.NewText("I can't take this anymore...", 0, 95, 215);
                                break;
                            case 240:
                                Main.NewText("All of this fighting is starting to tire me out some more...", 0, 95, 215);
                                break;
                            case 360:
                                Main.NewText("Even if you've won before, I am still powerful than you!", 0, 95, 215);
                                break;
                            case 480:
                                Main.NewText("Remember when I told you when I was an innocent being like you...", 0, 95, 215);
                                break;
                            case 600:
                                Main.NewText("I felt weak.", 0, 95, 215);
                                break;
                            case 720:
                                Main.NewText("Just like you when first came into this world...", 0, 95, 215);
                                break;
                            case 840:
                                Main.NewText("Nearly being enslaved by a powerful tyrant, I managed to escape his domain before everything got worse...", 0, 95, 215);
                                break;
                            case 960:
                                Main.NewText("Overtime I gained such strength thanks to a power blessing gifted by one of the most powerful beings, known to man!", 0, 95, 215);
                                break;
                            case 1080:
                                Main.NewText("I wanted to be as powerful as the devourer himself...", 0, 95, 215);
                                break;
                            case 1120:
                                Main.NewText("Only to lie in a field of cosmic pasture that has struck this land within...", 0, 95, 215);
                                break;
                            case 1240:
                                Main.NewText("I wanted to gain the power that the god eating entity himself has...", 0, 95, 215);
                                break;
                            case 1360:
                                Main.NewText("I Constructed a shrine in hopes someone or something empowers me more...", 0, 95, 215);
                                break;
                            case 1480:
                                Main.NewText("I later then planned my revenge on the tyrant himself, I wanted him gone!", 0, 95, 215);
                                break;
                            case 1720:
                                Main.NewText("Anywho " + player.name + ", should we pretend that all of the fighting you and I went through didn't happen?", 0, 95, 215);
                                break;
                        }
                    }
                    else
                    {
                        npc.velocity.Y += 0.01f;
                        npc.noTileCollide = false;

                        switch (introTimer)
                        {
                            case 0:
                                Main.NewText("HALT!", 0, 95, 215);
                                break;
                            case 120:
                                Main.NewText("I can't take this anymore...", 0, 95, 215);
                                break;
                            case 240:
                                Main.NewText("All of this fighting is starting to tire me out...", 0, 95, 215);
                                break;
                            case 360:
                                Main.NewText("Even if you outpower me... I an still, stronger than you think!", 0, 95, 215);
                                break;
                            case 480:
                                Main.NewText("When I was an Innocent being like you...", 0, 95, 215);
                                break;
                            case 600:
                                Main.NewText("I felt weak.", 0, 95, 215);
                                break;
                            case 720:
                                Main.NewText("Just like you when first came into this world...", 0, 95, 215);
                                break;
                            case 840:
                                Main.NewText("Nearly being enslaved by a powerful tyrant, I managed to escape his domain before everything got worse...", 0, 95, 215);
                                break;
                            case 960:
                                Main.NewText("Overtime I gained such strength thanks to a power blessing gifted by one of the most powerful beings, known to man!", 0, 95, 215);
                                break;
                            case 1080:
                                Main.NewText("I wanted to be as powerful as the devourer himself...", 0, 95, 215);
                                break;
                            case 1120:
                                Main.NewText("Only to lie in a field of cosmic pasture that has struck this land within...", 0, 95, 215);
                                break;
                            case 1240:
                                Main.NewText("I wanted to gain the power that the god eating entity himself has...", 0, 95, 215);
                                break;
                            case 1360:
                                Main.NewText("I Constructed a shrine in hopes someone or something empowers me more...", 0, 95, 215);
                                break;
                            case 1480:
                                Main.NewText("I later then planned my revenge on the tyrant himself, I wanted him gone!", 0, 95, 215);
                                break;
                            case 1720:
                                Main.NewText("However, my plan will arise when the stars above align, but for now I will be with you until then...", 0, 95, 215);
                                break;
                        }
                    }
                    if (introTimer >= 1720)
                    {
                        npc.dontTakeDamage = false;
                        bossIntro = false;
                        introTimer = 0;
                        tohouAttack = true;
                        npc.noTileCollide = true;
                    }
                }
                else
                {
                    switch (introTimer)
                    {
                        case 120:
                            Main.NewText("Behold...", 0, 95, 215);
                            break;
                        case 240:
                            Main.NewText("This is what ultra instinct looks like!", 0, 95, 215);
                            break;
                        case 360:
                            Main.NewText("And now...", 0, 95, 215);
                            break;
                        case 480:
                            Main.NewText("Let's see how you fare against me in this form!", 0, 95, 215);
                            break;
                    }
                    if (introTimer >= 480)
                    {
                        npc.dontTakeDamage = false;
                        bossIntro = false;
                        introTimer = 0;
                        tohouAttack = true;
                    }
                }
            }
            else
            {
                attackTimer++;

                if (npc.life < npc.lifeMax / 3)
                {
                    if (Phase == 0)
                    {
                        Phase = 1;
                        bossIntro = true;
                    }
                }

                if (!tohouAttack)
                {
                    tohouTimer = 0;

                    #region Attacks
                    if (attackTimer == 100 || attackTimer == 140 || attackTimer == 180 || attackTimer == 240)
                    {
                        int amountOfClones = Main.rand.Next(2, 6);
                        for (int i = 0; i < amountOfClones; ++i)
                        {
                            NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-200, 200), (int)npc.Center.Y + Main.rand.Next(-200, 200), ModContent.NPCType<CosmicEmperorShadowClone>());
                        }
                    }
                    if (attackTimer == 280 || attackTimer == 320 || attackTimer == 360 || attackTimer == 400)
                    {
                        int amountOfProjectiles = Main.rand.Next(2, 6);
                        for (int i = 0; i < amountOfProjectiles; ++i)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ModContent.ProjectileType<StarBomb>(), 12, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    else if (attackTimer == 415)
                    {
                        attackTimer = 0;
                    }
                    #endregion
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
                        if (Phase == 0)
                        {
                            Main.NewText("Alright, let's continue this battle now!", 0, 95, 215);
                        }
                        if (Phase == 1)
                        {
                            Main.NewText("Now you're really ticking me off! Why can't you just stand still and let me kill you?", 0, 95, 215);
                        }
                    }
                }
            }
        }

        public override bool PreAI()
        {
            if (!bossIntro)
            {
                float speed;
                if (EternalWorld.hellMode)
                {
                    speed = 96f;
                }
                else
                {
                    speed = 24f;
                }
                float acceleration = 0.10f;
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
            }

            return true;
        }

        public override void NPCLoot()
        {

            //Main.NewText("You've done well... I feel like you should be ready for my true power!", 0, 95, 215);
            //NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicEmperor>());

            if (EternalWorld.downedCosmicEmperor)
                Main.NewText("I hope we meet again one day...", 0, 95, 215);
            else
                Main.NewText("I am aware that you and I may see each other again one day for some time.", 0, 95, 215);

            if (!EternalWorld.downedCosmicEmperor)
                EternalWorld.downedCosmicEmperor = true;

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InterstellarMetal>(), Main.rand.Next(30, 60));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CosmoniumFragment>(), Main.rand.Next(30, 60));

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
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FinalBossBlade>());
                }
            }
        }
    }
}
