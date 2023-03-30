using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.SubzeroElemental
{
    [AutoloadBossHead]
    public class SubzeroElemental : ModNPC
    {
        int attackTimer = 0;
        int teleportTimer = 0;

        bool justSpawned = false;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

                new FlavorTextBestiaryInfoElement("A frostborn cryogenic construct that has once explored the world outside the tundra, freezing anything in it's path, however he sometimes lacks in strength when outside his domain...")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 82;
            NPC.lifeMax = 48000;
            NPC.damage = 50;
            NPC.boss = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.aiStyle = -1;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = MusicID.Boss3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 52000;
                NPC.damage = 70;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 54000;
                NPC.damage = 80;
            }
            else if (DifficultySystem.sinstormMode)
            {
                NPC.lifeMax = 58000;
                NPC.damage = 90;
            }
            else
            {
                NPC.lifeMax = 50000;
                NPC.damage = 60;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "Welp, I knew I was always weak...", dramatic: true);
                Main.NewText("Welp, I knew I was always weak...", 0, 90, 210);

                int gore1 = Mod.Find<ModGore>("SubzeroElementalArm").Type;
                int gore2 = Mod.Find<ModGore>("SubzeroElementalBody").Type;
                int gore3 = Mod.Find<ModGore>("SubzeroElementalHead").Type;
                int gore4 = Mod.Find<ModGore>("SubzeroElementalStave").Type;

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                }
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore4);
            }
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 25; k++)
                    Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Frost);
            }
        }

        public override bool PreAI()
        {
            var entitySource = NPC.GetSource_FromAI();

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            if (!justSpawned)
            {
                int robe1 = Mod.Find<ModGore>("RobeArm").Type;
                int robe2 = Mod.Find<ModGore>("RobeBody").Type;
                int robe3 = Mod.Find<ModGore>("RobeHood").Type;

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), robe1);
                }
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), robe2);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), robe3);

                justSpawned = true;
            }

            float speed = 20f;
            float acceleration = 0.25f;
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

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];
            Vector2 targetPosition = Main.player[NPC.target].position;

            #region Death Dialogue
            if (player.dead)
            {
                switch (Main.rand.Next(6))
                {
                    case 0:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "Well, that was fun! I wish we could do that again, maybe when you're less dead at least...", dramatic: true);
                        Main.NewText("Well, that was fun! I wish we could do that again, maybe when you're less dead at least...", 0, 90, 210);
                        break;
                    case 1:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "Oh... You're dead? Well that sucks...", dramatic: true);
                        Main.NewText("Oh... You're dead? Well that sucks...", 0, 90, 210);
                        break;
                    case 2:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "I'm very sure Incinerius did a much better job at this than you unfortunatley...", dramatic: true);
                        Main.NewText("I'm very sure Incinerius did a much better job at this than you unfortunatley...", 0, 90, 210);
                        break;
                    case 3:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "It's going to take a long time to see where this could go, unless you have a general idea on how this will work...", dramatic: true);
                        Main.NewText("It's going to take a long time to see where this could go, unless you have a general idea on how this will work...", 0, 90, 210);
                        break;
                    case 4:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "I guess the Frost King will be very dissapointed in you when you face him!", dramatic: true);
                        Main.NewText("I guess the Frost King will be very dissapointed in you when you face him!", 0, 90, 210);
                        break;
                    case 5:
                        CombatText.NewText(NPC.Hitbox, new Color(0, 90, 210), "This should teach you why you souldn't be second thinking around here.", dramatic: true);
                        Main.NewText("This should teach you why you souldn't be second thinking around here.", 0, 90, 210);
                        break;
                }
                NPC.active = false;
            }
            #endregion

            attackTimer++;

            teleportTimer++;
            if (Main.expertMode)
            {
                if (teleportTimer > 500)
                {
                    NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                    teleportTimer = 0;
                }
            }
            else if (DifficultySystem.hellMode)
            {
                if (teleportTimer > 250)
                {
                    NPC.position.X = targetPosition.X + Main.rand.Next(-200, 200);
                    teleportTimer = 0;
                }
            }
            else
            {
                if (teleportTimer > 750)
                {
                    NPC.position.X = targetPosition.X + Main.rand.Next(-600, 600);
                    teleportTimer = 0;
                }
            }

            if (attackTimer > 100 && attackTimer < 200)
            {
                CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.FrostBlastHostile, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Main.projectile[index5].tileCollide = false;
                Main.projectile[index5].timeLeft = 300;
            }

            if (attackTimer == 300 || attackTimer == 310 || attackTimer == 320 || attackTimer == 330)
            {
                if (Main.expertMode)
                {
                    for (int i = 0; i < Main.rand.Next(12, 24); i++)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(new Vector2(10f, 0.0f), (double)MathHelper.ToRadians((i * 30 + Main.rand.Next(30))), new Vector2()), ProjectileID.FrostBlastHostile, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }
                else if (DifficultySystem.hellMode)
                {
                    for (int i = 0; i < Main.rand.Next(24, 48); i++)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(new Vector2(10f, 0.0f), (double)MathHelper.ToRadians((i * 30 + Main.rand.Next(30))), new Vector2()), ProjectileID.FrostBlastHostile, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }
                else
                {
                    for (int i = 0; i < Main.rand.Next(6, 12); i++)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(new Vector2(10f, 0.0f), (double)MathHelper.ToRadians((i * 30 + Main.rand.Next(30))), new Vector2()), ProjectileID.FrostBlastHostile, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }
                }
            }

            if (attackTimer > 500)
            {
                attackTimer = 0;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;

            name = "The " + name;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
