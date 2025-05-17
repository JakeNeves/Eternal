using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    [AutoloadBossHead]
    public class Infernito : ModNPC
    {
        int aiInfernitoShootTime = 2;
        int AiInfernitoShootRate()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 8;
            else if (Main.expertMode)
                rate = 12;
            else
                rate = 16;

            return rate;
        }

        static int aiInfernitoLaserShotRateMax = 3;
        int aiInfernitoLaserShotRate = aiInfernitoLaserShotRateMax;

        float aiInfernitoProjectileRotation = MathHelper.PiOver2;

        ref float AttackTimer => ref NPC.ai[1];

        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 122;
            NPC.height = 160;
            NPC.lifeMax = 250000;
            NPC.defense = 60;
            NPC.damage = 30;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/TrinityHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/TrinityDeath");
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TrinitalEmbodiment");
            NPC.npcSlots = 6;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
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
                NPC.alpha = 255;

                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;

                if (NPC.alpha > 0)
                    NPC.alpha -= 5;

                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }

            if (NPC.ai[0] == 0)
            {
                #region Flying Movement
                float speed = 15f;
                float acceleration = 0.05f;
                Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
                if (length > 400 && Main.expertMode)
                {
                    ++speed;
                    acceleration += 0.1F;
                    if (length > 600)
                    {
                        ++speed;
                        acceleration += 0.15F;
                        if (length > 800)
                        {
                            ++speed;
                            acceleration += 0.2F;
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

            return true;
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            if (!NPC.AnyNPCs(ModContent.NPCType<Thunderius>()))
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    AI_Infernito_Attacks_Phase2();
                }
                else
                {
                    AI_Infernito_Attacks_Phase1();
                }
            }
        }

        private void AI_Infernito_Attacks_Phase1()
        {
            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (AttackTimer >= 200 && AttackTimer < 800)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiInfernitoProjectileRotation += 0.01f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                        SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);

                        aiInfernitoShootTime = AiInfernitoShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 5).RotatedBy(aiInfernitoProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1f)
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

            if (AttackTimer >= 900 && AttackTimer < 1000)
            {
                aiInfernitoLaserShotRate--;

                if (aiInfernitoLaserShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 300;
                    }

                    aiInfernitoLaserShotRate = aiInfernitoLaserShotRateMax;
                }
            }

            if (AttackTimer >= 1100 && AttackTimer < 1300)
            {
                if (Main.rand.NextBool(6))
                {
                    var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                    var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-20f, -15f));
                    int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4, 1f);
                    Main.projectile[i].hostile = true;
                    Main.projectile[i].tileCollide = true;
                    Main.projectile[i].friendly = false;
                }

                if (Main.rand.NextBool(3))
                {
                    var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                    var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-20f, -15f));
                    int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4, 1f);
                    Main.projectile[i].hostile = true;
                    Main.projectile[i].tileCollide = true;
                    Main.projectile[i].friendly = false;
                }
            }

            if (AttackTimer >= 1400 && AttackTimer < 1550)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiInfernitoProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiInfernitoShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(aiInfernitoProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.DeathLaser, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.DeathLaser, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                        int[] k = [
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel, ProjectileID.DeathLaser, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.DeathLaser, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.Pi), ProjectileID.DeathLaser, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(-MathHelper.PiOver2),ProjectileID.DeathLaser, NPC.damage / 2, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[k[l]].tileCollide = false;
                            Main.projectile[k[l]].timeLeft = 600;
                        }
                    }
                }

                if (DifficultySystem.hellMode)
                {
                    aiInfernitoLaserShotRate--;

                    if (aiInfernitoLaserShotRate <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, NPC.damage, 1, Main.myPlayer, 0, 0);

                            Main.projectile[proj].tileCollide = false;
                            Main.projectile[proj].timeLeft = 300;
                        }

                        aiInfernitoLaserShotRate = aiInfernitoLaserShotRateMax;
                    }
                }
            }

            if (AttackTimer >= 1600 && AttackTimer < 1800)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                aiInfernitoLaserShotRate--;

                if (aiInfernitoLaserShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                        SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 50;
                    }

                    aiInfernitoLaserShotRate = aiInfernitoLaserShotRateMax;
                }
            }

            if (AttackTimer >= 2000 && AttackTimer < 2450)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                aiInfernitoLaserShotRate--;

                if (aiInfernitoLaserShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Sanguinebeam2>(), NPC.damage, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                        Main.projectile[proj].timeLeft = 100;
                    }

                    aiInfernitoLaserShotRate = aiInfernitoLaserShotRateMax;
                }
            }

            if (AttackTimer > 2000)
            {
                AttackTimer = 0;
            }
        }

        private void AI_Infernito_Attacks_Phase2()
        {
            AttackTimer++;

            aiInfernitoLaserShotRateMax = 6;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
            float B = (float)Main.rand.Next(-200, 200) * 0.01f;

            if (AttackTimer >= 200 && AttackTimer < 500)
            {
                if (Main.rand.NextBool(6))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            SoundEngine.PlaySound(SoundID.Item167, NPC.Center);
                            SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.Center);
                        }

                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-20f, -15f));
                        int j = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage / 4, 1f);
                        Main.projectile[j].hostile = true;
                        Main.projectile[j].tileCollide = true;
                        Main.projectile[j].friendly = false;
                    }
                }
            }

            if (AttackTimer >= 750 && AttackTimer < 1000)
            {
                var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-20f, -15f));
                int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 4, 1f);
                Main.projectile[i].tileCollide = true;
                Main.projectile[i].timeLeft = 100;
            }

            if (AttackTimer >= 1250 && AttackTimer < 1500)
            {
                NPC.velocity = new Vector2(0f, 0f);
                NPC.rotation = 0;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiInfernitoProjectileRotation += 0.05f;
                    if (--aiInfernitoShootTime <= 0)
                    {
                        aiInfernitoShootTime = AiInfernitoShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 2).RotatedBy(aiInfernitoProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.CultistBossFireBall, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.CultistBossFireBall, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.CultistBossFireBall, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.CultistBossFireBall, NPC.damage, 1f)
                        ];
                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].tileCollide = false;
                            Main.projectile[i[j]].timeLeft = 600;
                        }
                        int[] k = [
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel, ProjectileID.CultistBossFireBall, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.CultistBossFireBall, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(MathHelper.Pi), ProjectileID.CultistBossFireBall, NPC.damage / 2, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, -shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.CultistBossFireBall, NPC.damage / 2, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[k[l]].tileCollide = false;
                            Main.projectile[k[l]].timeLeft = 600;
                        }
                    }
                }
            }

            if (AttackTimer > 1600)
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
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/Trinity/BodyEffigy").Value;

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
