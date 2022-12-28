using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    //[AutoloadBossHead]
    public class Flaremutaria : ModNPC
    {
        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetDefaults()
        {
            NPC.width = 138;
            NPC.height = 172;
            NPC.lifeMax = 2000000;
            NPC.defense = 70;
            NPC.damage = 110;
            NPC.aiStyle = -1;
            NPC.knockBackResist = -1f;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/TrinityHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            //NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/TrinityDeath");
            //NPC.DeathSound = SoundID.NPCDeath44;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            Music = MusicID.LunarBoss;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 4000000;
                NPC.defense = 80;
                NPC.damage = 114;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 5000000;
                NPC.defense = 85;
                NPC.damage = 116;
            }
            else
            {
                NPC.lifeMax = 3000000;
                NPC.defense = 75;
                NPC.damage = 112;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PerfectHealingPotion>();
        }

        public override bool PreAI()
        {
            if (!justSpawned)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.Torch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                justSpawned = true;
            }

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            NPC.rotation = NPC.velocity.X * 0.01f;

            if (NPC.AnyNPCs(ModContent.NPCType<Thunderius>()))
            {
                NPC.position.X = player.position.X + 600;
                NPC.position.Y = player.position.Y - 300;

                NPC.dontTakeDamage = true;

                int maxdusts = 6;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 200;
                    float dustSpeed = 8;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.Torch, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }
            else
            {
                NPC.dontTakeDamage = false;

                #region movement when Thunderius killed
                if (NPC.ai[0] == 0)
                {
                    #region Flying Movement
                    float speed = 20f;
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
                    #endregion
                }
                #endregion
            }

            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
