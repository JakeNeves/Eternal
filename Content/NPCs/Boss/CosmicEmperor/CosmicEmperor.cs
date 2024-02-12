using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Projectiles.Boss;
using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Content.BossBarStyles;

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    [AutoloadBossHead]
    public class CosmicEmperor : ModNPC
    {
        int tohouTimer = 0;
        int attackTimer = 0;
        int dialogueTimer = 0;

        bool spawnedClones = false;
        bool dialogue = false;
        bool tohouAttack = false;
        bool firstAttack = false;
        bool isDead = false;
        bool dontKillyet = false;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 54;
            NPC.height = 56;
            NPC.lifeMax = 400000;
            NPC.defense = 90;
            NPC.damage = 40;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = null;
            NPC.boss = true;
            Music = MusicID.OtherworldlyLunarBoss;
            NPC.knockBackResist = -1f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!dontKillyet)
            {
                if (NPC.life < 0)
                {
                    NPC.life = 1;
                    isDead = true;
                }
            }
        }

        /*public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (damage > NPC.lifeMax / 2)
            {
                SoundEngine.PlaySound(SoundID.DD2_BallistaTowerShot);
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
        }*/

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override bool PreAI()
        {
            #region boundry circle
            int maxDist = 1500;

            // ripped from another mod, credit to the person who wrote this
            for (int i = 0; i < 120; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset, 0, 0, DustID.Shadowflame, 0, 0, 100)];
                dust.noGravity = true;
            }
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead && Vector2.Distance(player.Center, NPC.Center) > maxDist)
                {
                    Vector2 toTarget = new Vector2(NPC.Center.X - player.Center.X, NPC.Center.Y - player.Center.Y);
                    toTarget.Normalize();
                    float speed = Vector2.Distance(player.Center, NPC.Center) > maxDist + 500 ? 1f : 0.5f;
                    player.velocity += toTarget * 0.5f;

                    player.dashDelay = 2;
                    player.grappling[0] = -1;
                    player.grapCount = 0;
                    for (int p = 0; p < Main.projectile.Length; p++)
                    {
                        if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                        {
                            Main.projectile[p].Kill();
                        }
                    }
                }
            }
            int maxdusts = 6;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 80;
                float dustSpeed = 8;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.DemonTorch, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
            #endregion
            return true;
        }

        public override void AI()
        {
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                {
                    EternalBossBarOverlay.SetTracked("The Nameless Tyrant Unleashed", NPC);
                    EternalBossBarOverlay.visible = true;
                }
            }

            if (!firstAttack)
            {
                tohouAttack = true;
                firstAttack = true;
            }

            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            attackTimer++;

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (tohouAttack)
            {
                NPC.dontTakeDamage = true;

                tohouTimer++;

                if (DifficultySystem.hellMode)
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(2, 4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), Main.rand.Next(-4, -2), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, Main.rand.Next(2, 14), ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, Main.rand.Next(-4, -2), ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, Main.rand.Next(2, 4), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, Main.rand.Next(-4, -2), 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(2, 4), ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, Main.rand.Next(-4, -2), ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                else
                {
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 2, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), -2, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 75 || attackTimer == 180 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, 2, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, 0, -2, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115 || attackTimer == 120)
                    {
                        for (int i = 0; i < 2; ++i)
                        {
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 2, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y + 800, -2, 0, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X - 800, player.position.Y + Main.rand.Next(-600, 600), 0, 2, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile(entitySource, player.position.X + 800, player.position.Y + Main.rand.Next(-600, 600), 0, -2, ModContent.ProjectileType<CosmicEmperorTohou>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
                if (attackTimer > 120)
                {
                    attackTimer = 0;
                }

                if (tohouTimer == 1500)
                {
                    NPC.dontTakeDamage = false;
                    tohouAttack = false;
                    tohouTimer = 0;
                }
            }
            else
            {
                if (attackTimer == 75 || attackTimer == 100 || attackTimer == 125 || attackTimer == 150 || attackTimer == 175 || attackTimer == 200)
                {
                    for (int i = 0; i < 3 + Main.rand.Next(3); ++i)
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 375 || attackTimer == 400 || attackTimer == 425 || attackTimer == 450 || attackTimer == 475 || attackTimer == 500)
                {
                    for (int i = 0; i < 4 + Main.rand.Next(4); ++i)
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit3, NPC.Center);
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicFireball>(), NPC.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer > 500 && attackTimer < 700)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ModContent.ProjectileType<CosmicEmperorKnife>(), NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<CosmicEmperorKnife>(), NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].tileCollide = false;
                    Main.projectile[index5].timeLeft = 300;
                    Main.projectile[index6].tileCollide = false;
                    Main.projectile[index6].timeLeft = 300;
                }
                if (attackTimer == 800)
                {
                    for (int i = 0; i < Main.rand.Next(18, 36); i++)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(new Vector2(10f, 0.0f), (double)MathHelper.ToRadians((i * 30 + Main.rand.Next(30))), new Vector2()), ModContent.ProjectileType<CosmicFireball>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }
                if (attackTimer == 1000)
                {
                    Projectile.NewProjectile(entitySource, player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<EmperorBlade>(), NPC.damage, 0, Main.myPlayer, 0f, 0f);

                    attackTimer = 0;
                }
            }

            if (isDead)
            {
                attackTimer = 0;
                NPC.dontTakeDamage = true;

                if (!dialogue)
                    dialogueTimer++;
                else
                {
                    dialogueTimer = 0;
                }
                switch (dialogueTimer)
                {
                    case 150:
                        Main.NewText("Well, it looks like you're pretty good...", 150, 36, 120);
                        break;
                    case 290:
                        Main.NewText("However, this is the end of your journey, because guess what...", 150, 36, 120);
                        break;
                    case 450:
                        Main.NewText("YOUR RESISTANCE ENDS NOW!", 150, 36, 120);
                        break;
                    case 690:
                        Main.NewText("I CAN ALREADY HEAR THE MARCHING OF A THOUSAND ANTS APPROACHING!", 150, 36, 120);
                        break;
                    case 850:
                        Main.NewText("CLONES, PUNISH THEM!", 150, 36, 120);
                        spawnedClones = true;
                        for (int i = 0; i < 12; ++i)
                        {
                            NPC.NewNPC(entitySource, (int)NPC.position.X + Main.rand.Next(-120, 120), (int)NPC.position.Y + Main.rand.Next(-120, 120), ModContent.NPCType<CosmicEmperorClone>());
                        }
                        dialogue = true;
                        break;
                }
                if (spawnedClones)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<CosmicEmperorClone>()))
                    {
                        Main.NewText("THESE CLONES ARE USELESS, I WILL HAVE TO TAKE MATTER INTO MY OWN HANDS NOW!", 150, 36, 120);
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<CosmicEmperorP2>());
                        NPC.active = false;
                    }
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
