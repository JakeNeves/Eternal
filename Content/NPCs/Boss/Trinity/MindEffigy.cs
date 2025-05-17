using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    public class MindEffigy : ModNPC
    {
        int aiThunderiusShootTime = 2;
        int AiThunderiusShootRate()
        {
            int rate = 12;

            if (DifficultySystem.hellMode)
                rate = 4;
            else if (Main.expertMode)
                rate = 8;
            else
                rate = 12;

            return rate;
        }

        float aiThunderiusProjectileRotation = MathHelper.PiOver2;

        const float Speed = 15f;
        const float Acceleration = 0.5f;
        
        ref float AttackTimer => ref NPC.ai[1];

        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 84;
            NPC.height = 84;
            NPC.lifeMax = 250000;
            NPC.defense = 70;
            NPC.damage = 30;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/TrinitySpiritHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/TrinitySpiritDeath");
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
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

            AttackTimer++;
            if (AttackTimer >= 0)
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
                if (Main.rand.NextBool(96) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    int targetTileX = (int)Main.player[NPC.target].Center.X / 16;
                    int targetTileY = (int)Main.player[NPC.target].Center.Y / 16;
                    Vector2 chosenTile = Vector2.Zero;
                    if (NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
                    {
                        NPC.ai[2] = chosenTile.X;
                        NPC.ai[3] = chosenTile.Y;
                    }

                    NPC.netUpdate = true;
                }
            }

            return true;
        }

        public override void AI()
        {
            Vector2 circDir = new Vector2(0f, 45f);

            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 0.5f, 1.06f, 2.55f);

            if (Main.rand.NextBool(24))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                AI_Thunderius_Attacks_Phase2();
            }
            else
            {
                AI_Thunderius_Attacks_Phase1();
            }
        }

        private void AI_Thunderius_Attacks_Phase1()
        {
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            Vector2 circDir = new Vector2(0f, 45f);

            if (AttackTimer == 250)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.HallowBossRainbowStreak, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
            }

            if (AttackTimer == 300 || AttackTimer == 305 || AttackTimer == 310 || AttackTimer == 315 || AttackTimer == 320)
            {
                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.Item84, NPC.position);

                    NPC.position = new Vector2(player.position.X + Main.rand.NextFloat(-600f, 600f), player.position.Y + Main.rand.NextFloat(-600f, 600f));

                    if (Main.rand.NextBool(12))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int projectile = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.HeatRay, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                            Main.projectile[projectile].hostile = true;
                            Main.projectile[projectile].friendly = false;
                            Main.projectile[projectile].timeLeft = 250;
                        }
                    }
                }
            }

            if (AttackTimer > 650)
            {
                AttackTimer = 0;

                for (int i = 0; i < 6; i++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }
        }

        private void AI_Thunderius_Attacks_Phase2()
        {
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            Vector2 circDir = new Vector2(0f, 60f);

            if (AttackTimer >= 200 && AttackTimer < 400)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiThunderiusProjectileRotation += 0.05f;
                    if (--aiThunderiusShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item103, NPC.position);

                        aiThunderiusShootTime = AiThunderiusShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 20).RotatedBy(aiThunderiusProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.HeatRay, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.HeatRay, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.HeatRay, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.HeatRay, NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].friendly = false;
                            Main.projectile[i[l]].hostile = true;
                            Main.projectile[i[l]].timeLeft = 300;
                        }
                    }
                }
            }

            if (AttackTimer >= 600 && AttackTimer < 650)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.BallofFire, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].timeLeft = 300;
                    }
                }
            }

            if (AttackTimer == 700 || AttackTimer == 705 || AttackTimer == 710 || AttackTimer == 715 || AttackTimer == 720)
            {
                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DD2BetsyFireball, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 250;
                    }
                }
            }

            if (AttackTimer >= 800 && AttackTimer < 1000)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.HeatRay, NPC.damage, 1, Main.myPlayer, 0, 0);

                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].tileCollide = false;
                    Main.projectile[proj].timeLeft = 100;
                }
            }

            if (AttackTimer > 1300)
            {
                AttackTimer = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}
