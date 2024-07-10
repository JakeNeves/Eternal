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
    public class Thunderius : ModNPC
    {

        #region Fundementals
        const int ShootType = ProjectileID.MartianTurretBolt;
        const int ShootDamage = 9;
        const float ShootKnockback = 0f;
        const int ShootDirection = 5;

        const float Speed = 14f;
        const float Acceleration = 0.2f;
        int Timer;
        #endregion

        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetDefaults()
        {
            NPC.width = 84;
            NPC.height = 84;
            NPC.lifeMax = 2000000;
            NPC.defense = 70;
            NPC.damage = 110;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
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

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
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
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.UltraBrightTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                justSpawned = true;
            }

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            NPC.rotation += NPC.velocity.X * 0.1f;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<Cryota>()))
            {
                NPC.position.X = player.position.X - 600;
                NPC.position.Y = player.position.Y - 300;

                NPC.dontTakeDamage = true;

                int maxdusts = 6;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 200;
                    float dustSpeed = 8;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.UltraBrightTorch, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }
            else
            {
                NPC.dontTakeDamage = false;

                #region movement when Cryota killed
                Timer++;
                if (Timer >= 0)
                {
                    Vector2 StartPosition = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    float DirectionX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - StartPosition.X;
                    float DirectionY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120 - StartPosition.Y;
                    float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                    float Num = Speed / Length;
                    DirectionX = DirectionX * Num;
                    DirectionY = DirectionY * Num;
                    if (NPC.velocity.X < DirectionX)
                    {
                        NPC.velocity.X = NPC.velocity.X + Acceleration;
                        if (NPC.velocity.X < 0 && DirectionX > 0)
                            NPC.velocity.X = NPC.velocity.X + Acceleration;
                    }
                    else if (NPC.velocity.X > DirectionX)
                    {
                        NPC.velocity.X = NPC.velocity.X - Acceleration;
                        if (NPC.velocity.X > 0 && DirectionX < 0)
                            NPC.velocity.X = NPC.velocity.X - Acceleration;
                    }
                    if (NPC.velocity.Y < DirectionY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                        if (NPC.velocity.Y < 0 && DirectionY > 0)
                            NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                    }
                    else if (NPC.velocity.Y > DirectionY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                        if (NPC.velocity.Y > 0 && DirectionY < 0)
                            NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                    }
                    if (Main.rand.NextBool(36))
                    {
                        Vector2 StartPosition2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                        float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), StartPosition2.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                        NPC.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                        NPC.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                        NPC.netUpdate = true;
                    }
                }
                #endregion
            }

            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.5f, 1.06f, 2.55f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
