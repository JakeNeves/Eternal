using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Potions;
using Eternal.Projectiles.Enemy;

namespace Eternal.NPCs.Boss.BionicBosses
{
    //[AutoloadBossHead]
    public class PolarusNeox : ModNPC
    {
        private Player player;

        int attackTimer = 0;
        int teleportTimer = 0;
        int frameNum;

        bool dronesSpwned = false;
        bool phase2Warn = false;
        bool canTeleport = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXM-2350 Polarus-N30X");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1240000;
            npc.damage = 80;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 240;
            npc.height = 178;
            npc.value = Item.buyPrice(platinum: 6, gold: 40);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/NeoxPower");
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
                npc.lifeMax = 6060000;
                npc.damage = 240;
                npc.defense = 90;
            }
        }

        /*public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINE EXR-2303...", dramatic: true);
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Electric, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Fire, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }*/

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            npc.netUpdate = true;
            player = Main.player[npc.target];
            npc.dontTakeDamage = false;
            float speed;
            if (!NPC.AnyNPCs(ModContent.NPCType<PolarusNeoxSignalDrone>()))
            {
                speed = 36.25f;
            }
            else
            {
                speed = 18.125f;
            }
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

            Vector2 targetPosition = Main.player[npc.target].position;
            if (canTeleport)
            {
                teleportTimer++;
            }
            if (EternalWorld.hellMode)
            {
                if (npc.life < npc.lifeMax / 2)
                {
                    if (teleportTimer >= 50)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 100)
                    {
                        Main.PlaySound(SoundID.Item8, Main.myPlayer);
                        npc.position.X = targetPosition.X + Main.rand.Next(-300, 300);
                        npc.position.Y = targetPosition.Y + Main.rand.Next(-300, 300);
                        teleportTimer = 0;
                    }
                }
                else
                {
                    if (teleportTimer >= 100)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 150)
                    {
                        Main.PlaySound(SoundID.Item8, Main.myPlayer);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                        npc.position.X = targetPosition.X + Main.rand.Next(-500, 500);
                        npc.position.Y = targetPosition.Y + Main.rand.Next(-500, 500);
                        teleportTimer = 0;
                    }
                }
            }
            else
            {
                if (npc.life < npc.lifeMax / 2)
                {
                    if (teleportTimer >= 100)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 150)
                    {
                        Main.PlaySound(SoundID.Item8, Main.myPlayer);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                        npc.position.X = targetPosition.X + Main.rand.Next(-450, 450);
                        npc.position.Y = targetPosition.Y + Main.rand.Next(-450, 450);
                        teleportTimer = 0;
                    }
                }
                else
                {
                    if (teleportTimer >= 150)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 200)
                    {
                        Main.PlaySound(SoundID.Item8, Main.myPlayer);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 1f, npc.oldVelocity.Y * 1f);
                        }
                        npc.position.X = targetPosition.X + Main.rand.Next(-500, 500);
                        npc.position.Y = targetPosition.Y + Main.rand.Next(-500, 500);
                        teleportTimer = 0;
                    }
                }
            }

            DespawnHandler();

            if (npc.life < npc.lifeMax / 2)
            {
                frameNum = 1;
                Lighting.AddLight(npc.Center, 1.17f, 0.10f, 0.32f);
            }
            else
            {
                frameNum = 0;
                Lighting.AddLight(npc.Center, 0f, 0f, 1.90f);
            }

            return true;
        }

        public override void AI()
        {
            if (!dronesSpwned)
            {
                int droneAmount;
                if (Main.expertMode)
                {
                    droneAmount = 48;
                }
                else if (EternalWorld.hellMode)
                {
                    droneAmount = 64;
                }
                else
                {
                    droneAmount = 32;
                }

                for (int i = 0; i < droneAmount; ++i)
                {
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-400, 400), (int)npc.Center.Y + Main.rand.Next(-400, 400), ModContent.NPCType<PolarusNeoxSignalDrone>());
                }

                dronesSpwned = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<PolarusNeoxSignalDrone>()))
            {
                npc.dontTakeDamage = true;

                canTeleport = false;
            }
            else
            {
                npc.dontTakeDamage = false;

                canTeleport = true;

                if (npc.life < npc.lifeMax / 2)
                {
                    if (!phase2Warn)
                    {
                        Main.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Main.myPlayer);
                        CombatText.NewText(npc.Hitbox, Color.Blue, "CRITICAL DAMAGE DETECTED, DEPLOYING EMERGENCY SIGNAL DRONES...", dramatic: true);
                        dronesSpwned = false;
                        phase2Warn = true;
                    }
                }
                else
                {
                    attackTimer++;
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70 || attackTimer == 75 || attackTimer == 80 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95 || attackTimer == 100)
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
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<PolarusNeoxSignalDroneLaser>(), npc.damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    if (EternalWorld.hellMode)
                    {
                        if (attackTimer == 150 || attackTimer == 155 || attackTimer == 160 || attackTimer == 165 || attackTimer == 170 || attackTimer == 175 || attackTimer == 180 || attackTimer == 185 || attackTimer == 190 || attackTimer == 195 || attackTimer == 200)
                        {

                        }
                    }
                    else
                    {
                        if (attackTimer == 150 || attackTimer == 160 || attackTimer == 170 || attackTimer == 180 || attackTimer == 190 || attackTimer == 200)
                        {

                        }
                    }
                    if (attackTimer == 300)
                    {
                        attackTimer = 0;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameNum * frameHeight;
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
