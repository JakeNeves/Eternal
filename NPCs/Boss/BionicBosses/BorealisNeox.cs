using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.NPCs.Boss.BionicBosses.Omicron;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    //[AutoloadBossHead]
    public class BorealisNeox : ModNPC
    {
        private Player player;

        int AttackTimer = 0;
        int Phase = 0;

        bool isDashing = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXR-2303 Borealis-N30X");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1240000;
            npc.damage = 80;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 114;
            npc.height = 324;
            npc.value = Item.buyPrice(platinum: 6, gold: 40);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = MusicID.LunarBoss;
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
            npc.lifeMax = 2530000;
            npc.damage = 120;
            npc.defense = 85;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6000000;
                npc.damage = 240;
                npc.defense = 90;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.LightBlue, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINE EXR-2308...", dramatic: true);
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
            Lighting.AddLight(npc.Center, 0.73f, 1.40f, 2.12f);

            npc.rotation = npc.velocity.X * 0.03f;

            npc.netUpdate = true;
            player = Main.player[npc.target];
            npc.dontTakeDamage = false;
            float speed = 36.25f;
            float acceleration = 0.4f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            if (isDashing)
            {
                if (EternalWorld.hellMode)
                {
                    speed = 48f;
                    acceleration = 0.16f;
                }
                else if (Main.expertMode)
                {
                    speed = 42.75f;
                    acceleration = 0.12f;
                }
                else
                {
                    speed = 38.50f;
                    acceleration = 0.8f;
                }

                for (int k = 0; k < 8; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 0.75f, npc.oldVelocity.Y * 0.75f);
                }

                npc.rotation = npc.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                speed = 32.25f;
                acceleration = 0.4f;
            }

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
            DespawnHandler();

            return true;
        }

        public override void AI()
        {
            AttackTimer++;

            if (npc.life < npc.lifeMax / 2)
            {
                if ((AttackTimer == 165 || AttackTimer == 170 || AttackTimer == 175 || AttackTimer == 180 || AttackTimer == 185 || AttackTimer == 190))
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
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (AttackTimer == 195)
                {
                    isDashing = true;
                }
                else if (AttackTimer == 300)
                {
                    AttackTimer = 0;
                    isDashing = false;
                }
            }
            if (npc.life < npc.lifeMax / 3)
            {
                if (AttackTimer == 50 || AttackTimer == 65 || AttackTimer == 70 || AttackTimer == 85 || AttackTimer == 90 || AttackTimer == 105 || AttackTimer == 115 || AttackTimer == 130)
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
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<BorealisNeoxSpike>(), damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if ((AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125))
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 4;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                else if (AttackTimer == 300)
                {
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage, 0, Main.myPlayer, 0f, 0f);

                    isDashing = false;
                    AttackTimer = 0;
                }
            }
            else
            {
                if ((AttackTimer == 100 || AttackTimer == 150 || AttackTimer == 175))
                {
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ModContent.ProjectileType<BorealisNeoxSpike>(), npc.damage, 0, Main.myPlayer, 0f, 0f);

                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;

                    int amountOfProjectiles = 1;
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<BorealisNeoxLaser>(), npc.damage / 2, 1, Main.myPlayer, 0, 0);
                    }
                }
                else if (AttackTimer == 475)
                {
                    AttackTimer = 0;
                }
            }
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
            player = Main.player[npc.target];
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<OmicronNeox>());

            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NeoxCore>(), Main.rand.Next(6, 16));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
