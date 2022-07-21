using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    //[AutoloadBossHead]
    public class Cryota : ModNPC
    {
        #region Fundementals
        const int ShootType = ProjectileID.MartianTurretBolt;
        const int ShootDamage = 9;
        const float ShootKnockback = 0f;
        const int ShootDirection = 5;

        const float Speed = 70f;
        const float Acceleration = 0.28f;
        int Timer;
        #endregion

        bool isDashing = false;
        bool justSpawned = false;

        int attackTimer = 0;

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public static int SentryCount()
        {
            int count = 3;

            if (Main.expertMode)
            {
                count += 6;
            }
            else if (DifficultySystem.hellMode)
            {
                if (ModContent.GetInstance<CommonConfig>().BrutalHellMode)
                {
                    count += 12;
                }
                else
                {
                    count += 9;
                }
            }

            return count;
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetDefaults()
        {
            NPC.width = 106;
            NPC.height = 184;
            NPC.lifeMax = 2000000;
            NPC.defense = 70;
            NPC.damage = 110;
            NPC.aiStyle = -1;
            NPC.knockBackResist = -1f;
            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath44;
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

            int count = SentryCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CryotasSentry>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is CryotasSentry minion)
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

        public override bool PreAI()
        {
            if (!justSpawned)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.IceTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                SpawnShield();
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

            if (isDashing)
            {
                NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90f);

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
            }
            else
            {
                NPC.rotation = NPC.velocity.X * 0.01f;

                if (NPC.ai[0] == 0)
                {
                    #region Flying Movement
                    float speed = 25f;
                    float acceleration = 0.24f;
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
            }

            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 1.64f, 2.36f, 2.55f);

            attackTimer++;

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            var entitySource = NPC.GetSource_FromAI();

            if (attackTimer == 100)
            {
                isDashing = true;
            }
            if (attackTimer == 500)
            {
                attackTimer = 0;
                isDashing = false;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
