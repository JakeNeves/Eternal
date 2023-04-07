using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.NeoxMechs
{
    //[AutoloadBossHead]
    public class Polarus : ModNPC
    {
        private Player player;

        int attackTimer = 0;
        int teleportTimer = 0;
        int frameNum;

        bool dronesSpwned = true;
        bool phase2Warn = false;
        bool canTeleport = false;

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public static int ShieldCount()
        {
            int count = 3;

            if (Main.expertMode)
            {
                count += 6;
            }
            else if (DifficultySystem.hellMode)
            {
                count += 9;
            }

            return count;
        }

        private void SpawnShield()
        {
            if (SpawnedShield)
            {
                return;
            }

            SpawnedShield = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int count = ShieldCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<PolarusShield>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is PolarusShield minion)
                {
                    minion.ParentIndex = NPC.whoAmI;
                    minion.PositionIndex = i;
                }

                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Polarus N30X");
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 2400000;
            NPC.damage = 80;
            NPC.defense = 30;
            NPC.knockBackResist = -1f;
            NPC.width = 194;
            NPC.height = 172;
            NPC.value = Item.buyPrice(platinum: 6, gold: 40);
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.alpha = 0;
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/NeoxPower");
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A sentient machine which resembles an otherworldly construct, this machine has the ability to teleport, hypnotize and create illusions.")
            });
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 4800000;
                NPC.damage = 120;
                NPC.defense = 60;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 9600000;
                NPC.damage = 140;
                NPC.defense = 75;
            }
            else if (DifficultySystem.sinstormMode)
            {
                NPC.lifeMax = 18000000;
                NPC.damage = 140;
                NPC.defense = 90;
            }
            else
            {
                NPC.lifeMax = 3600000;
                NPC.damage = 100;
                NPC.defense = 45;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                CombatText.NewText(NPC.Hitbox, Color.Red, "POWERING DOWN...", dramatic: true);
            }
            else
            {
                for (int k = 0; k < 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Electric, 0, -2f, 0, default(Color), 1f);
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PurpleTorch, 0, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            var entitySource = NPC.GetSource_FromAI();

            SpawnShield();

            NPC.netUpdate = true;
            player = Main.player[NPC.target];
            NPC.dontTakeDamage = false;
            float speed;
            if (!NPC.AnyNPCs(ModContent.NPCType<PolarusSignalDrone>()) && !NPC.AnyNPCs(ModContent.NPCType<PolarusShield>()))
            {
                speed = 36.25f;
            }
            else
            {
                speed = 18.125f;
            }
            float acceleration = 0.4f;
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

            Vector2 targetPosition = Main.player[NPC.target].position;
            if (canTeleport)
            {
                teleportTimer++;
            }
            if (DifficultySystem.hellMode)
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (teleportTimer >= 50)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 100)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        NPC.position.X = targetPosition.X + Main.rand.Next(-300, 300);
                        NPC.position.Y = targetPosition.Y + Main.rand.Next(-300, 300);
                        teleportTimer = 0;

                        if (NPC.life < NPC.lifeMax / 3)
                        {
                            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                            direction.Normalize();
                            direction.X *= 8.5f;
                            direction.Y *= 8.5f;
                            int amountOfProjectiles = 8;

                            for (int i = 0; i < amountOfProjectiles; ++i)
                            {
                                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    if (teleportTimer >= 100)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 150)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                        NPC.position.X = targetPosition.X + Main.rand.Next(-500, 500);
                        NPC.position.Y = targetPosition.Y + Main.rand.Next(-500, 500);
                        teleportTimer = 0;
                    }
                }
            }
            else
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (teleportTimer >= 100)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 150)
                    {
                       SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                        NPC.position.X = targetPosition.X + Main.rand.Next(-450, 450);
                        NPC.position.Y = targetPosition.Y + Main.rand.Next(-450, 450);
                        teleportTimer = 0;

                        if (NPC.life < NPC.lifeMax / 3)
                        {
                            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                            direction.Normalize();
                            direction.X *= 8.5f;
                            direction.Y *= 8.5f;
                            int amountOfProjectiles = 4;

                            for (int i = 0; i < amountOfProjectiles; ++i)
                            {
                                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    if (teleportTimer >= 150)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                    }
                    if (teleportTimer >= 200)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 1f, NPC.oldVelocity.Y * 1f);
                        }
                        NPC.position.X = targetPosition.X + Main.rand.Next(-500, 500);
                        NPC.position.Y = targetPosition.Y + Main.rand.Next(-500, 500);
                        teleportTimer = 0;
                    }
                }
            }

            DespawnHandler();

            return true;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            if (!dronesSpwned)
            {
                int droneAmount;
                if (Main.expertMode)
                {
                    droneAmount = 4;
                }
                else if (Main.masterMode)
                {
                    droneAmount = 6;
                }
                else if (DifficultySystem.hellMode)
                {
                    droneAmount = 8;
                }
                else
                {
                    droneAmount = 2;
                }

                for (int i = 0; i < droneAmount; ++i)
                {
                    NPC.NewNPC(entitySource, (int)NPC.Center.X + Main.rand.Next(-400, 400), (int)NPC.Center.Y + Main.rand.Next(-400, 400), ModContent.NPCType<PolarusSignalDrone>());
                }

                dronesSpwned = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<PolarusSignalDrone>()) && NPC.AnyNPCs(ModContent.NPCType<PolarusShield>()))
            {
                NPC.dontTakeDamage = true;

                canTeleport = false;
            }
            else
            {
                NPC.dontTakeDamage = false;

                canTeleport = true;

                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (!phase2Warn)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.Red, "CRITICAL DAMAGE DETECTED, DEPLOYING SHIELD AND DISPATCHING SIGNAL DRONES...", dramatic: true);
                        SpawnShield();
                        dronesSpwned = false;
                        phase2Warn = true;
                    }
                }
                else
                {
                    attackTimer++;
                    if (attackTimer == 50 || attackTimer == 55 || attackTimer == 60 || attackTimer == 65 || attackTimer == 70 || attackTimer == 75 || attackTimer == 80 || attackTimer == 85 || attackTimer == 90 || attackTimer == 95 || attackTimer == 100)
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
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    if (DifficultySystem.hellMode)
                    {
                        if (attackTimer == 150 || attackTimer == 155 || attackTimer == 160 || attackTimer == 165 || attackTimer == 170 || attackTimer == 175 || attackTimer == 180 || attackTimer == 185 || attackTimer == 190 || attackTimer == 195 || attackTimer == 200)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
                            for (int i = 0; i < 2; ++i)
                            {
                                Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, Main.rand.Next(36, 96), ProjectileID.BombSkeletronPrime, 50, 0, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                    else
                    {
                        if (attackTimer == 150 || attackTimer == 160 || attackTimer == 170 || attackTimer == 180 || attackTimer == 190 || attackTimer == 200)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, NPC.Center);
                            for (int i = 0; i < 2; ++i)
                            {
                                Projectile.NewProjectile(entitySource, player.position.X + Main.rand.Next(-600, 600), player.position.Y - 800, 0, 36, ProjectileID.BombSkeletronPrime, 50, 0, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                    if (attackTimer == 300)
                    {
                        attackTimer = 0;
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            // Brin of Cthulu things
            Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            float num66 = 0f;
            Vector2 vector11 = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[NPC.type] / 2));
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Microsoft.Xna.Framework.Rectangle frame6 = NPC.frame;
            Microsoft.Xna.Framework.Color alpha15 = NPC.GetAlpha(color9);
            float alpha = 1.25f * (1f - (float)NPC.life / (float)NPC.lifeMax);
            alpha *= alpha;
            alpha = Math.Min(alpha, 1);
            alpha15.R = (byte)((float)alpha15.R * alpha);
            alpha15.G = (byte)((float)alpha15.G * alpha);
            alpha15.B = (byte)((float)alpha15.B * alpha);
            alpha15.A = (byte)((float)alpha15.A * alpha);
            for (int num213 = 0; num213 < 4; num213++)
            {
                Vector2 position9 = NPC.position;
                float num214 = Math.Abs(NPC.Center.X - Main.player[Main.myPlayer].Center.X);
                float num215 = Math.Abs(NPC.Center.Y - Main.player[Main.myPlayer].Center.Y);
                if (num213 == 0 || num213 == 2)
                {
                    position9.X = Main.player[Main.myPlayer].Center.X + num214;
                }
                else
                {
                    position9.X = Main.player[Main.myPlayer].Center.X - num214;
                }
                position9.X -= (float)(NPC.width / 2);
                if (num213 == 0 || num213 == 1)
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
                }
                else
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
                }
                position9.Y -= (float)(NPC.height / 2);
                Main.spriteBatch.Draw(texture, new Vector2(position9.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)texture.Width * NPC.scale / 2f + vector11.X * NPC.scale, position9.Y - Main.screenPosition.Y + (float)NPC.height - (float)texture.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)texture.Width * NPC.scale / 2f + vector11.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)texture.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            return false;
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
