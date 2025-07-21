using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Trinity
{
    [AutoloadBossHead]
    public class Cryota : ModNPC
    {
        bool justSpawned = false;

        ref float AttackTimer => ref NPC.ai[1];

        float aiCryotaProjectileRotation = MathHelper.PiOver2;

        static int aiCryotaShotRateMax = 12;
        int aiCryotaShotRate = aiCryotaShotRateMax;

        Vector2 CircleDirc = new Vector2(0.0f, 15f);

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        int aiCryotaShootTime = 4;
        int AiCryotaShootRate()
        {
            int rate;

            if (DifficultySystem.hellMode)
                rate = 12;
            else if (Main.expertMode)
                rate = 24;
            else
                rate = 36;

            return rate;
        }

        public static int SentryCount()
        {
            int count = 4;

            if (Main.expertMode)
            {
                count += 6;
            }
            else if (DifficultySystem.hellMode)
            {
                count += 8;
            }

            return count;
        }

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
            NPC.width = 94;
            NPC.height = 94;
            NPC.lifeMax = 150000;
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

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            Main.NewText("The Trinity has revealed it's true self!", 175, 75, 255);
            NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TrinityCore>(), NPC.whoAmI);
        }

        public override bool PreAI()
        {
            if (!justSpawned)
            {
                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.IceTorch);
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

            #region Flying Movement
            float speed = 10f;
            float acceleration = 0.02f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.04F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.06F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.08F;
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

            return true;
        }

        public override void AI()
        {
            if(!Main.dedServ)
                Lighting.AddLight(NPC.position, 1.64f, 2.36f, 2.55f);

            if (NPC.AnyNPCs(ModContent.NPCType<CryotasSentry>()) || NPC.AnyNPCs(ModContent.NPCType<Infernito>()))
            {
                NPC.alpha = 255;

                NPC.dontTakeDamage = true;
            }
            else
            {
                SpawnShield();

                NPC.dontTakeDamage = false;

                if (NPC.alpha > 0)
                    NPC.alpha -= 5;

                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }

            if (!NPC.AnyNPCs(ModContent.NPCType<CryotasSentry>()) && !NPC.AnyNPCs(ModContent.NPCType<Infernito>()))
            {
                if (NPC.life < NPC.lifeMax / 2)
                {
                    AI_Cryota_Attacks_Phase2();
                }
                else
                {
                    AI_Cryota_Attacks_Phase1();
                }
            }
        }

        private void AI_Cryota_Attacks_Phase1()
        {
            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-150, 150) * 0.01f;
            float B = (float)Main.rand.Next(-150, 150) * 0.01f;

            if (AttackTimer >= 150 && AttackTimer < 200)
            {
                aiCryotaShotRate--;

                if (aiCryotaShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].timeLeft = 100;
                        Main.projectile[index6].timeLeft = 100;
                    }
                }
            }

            if (AttackTimer == 300)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CryotasWisp>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].tileCollide = false;
                }
            }

            if (AttackTimer >= 350 && AttackTimer < 400)
            {
                aiCryotaShotRate--;

                if (aiCryotaShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item84, NPC.position);

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CryotasWisp2>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 450 && AttackTimer < 600)
            {
                NPC.velocity = new Vector2(0f, 0f);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiCryotaProjectileRotation += 0.01f;
                    if (--aiCryotaShootTime <= 0)
                    {
                        aiCryotaShootTime = AiCryotaShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 5).RotatedBy(aiCryotaProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 150;
                        }
                    }
                }
            }

            if (AttackTimer == 650)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CryotasWisp>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].tileCollide = false;
                }
            }

            if (AttackTimer > 660)
                AttackTimer = 0;
        }

        private void AI_Cryota_Attacks_Phase2()
        {
            AttackTimer++;

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            float A = (float)Main.rand.Next(-150, 150) * 0.01f;
            float B = (float)Main.rand.Next(-150, 150) * 0.01f;

            if (AttackTimer == 100)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CryotasWisp>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].tileCollide = false;
                }
            }

            if (AttackTimer >= 150 && AttackTimer < 200)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiCryotaProjectileRotation += 0.01f;
                    if (--aiCryotaShootTime <= 0)
                    {
                        aiCryotaShootTime = AiCryotaShootRate();
                        var shootPos = NPC.Center;
                        var shootVel = new Vector2(0, 5).RotatedBy(aiCryotaProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CryotasWisp>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<CryotasWisp>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<CryotasWisp>(), NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<CryotasWisp>(), NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                            Main.projectile[i[l]].timeLeft = 150;
                        }
                    }
                }
            }

            if (AttackTimer >= 200 && AttackTimer < 400)
            {
                if (Main.rand.NextBool(6))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                        var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(15f, 20f));
                        int j = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<CryotasWisp2>(), NPC.damage / 4, 1f);
                        Main.projectile[j].hostile = true;
                        Main.projectile[j].tileCollide = true;
                        Main.projectile[j].friendly = false;
                    }
                }
            }

            if (AttackTimer >= 450 && AttackTimer < 500)
            {
                aiCryotaShotRate--;

                if (aiCryotaShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<CryotasWisp2>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].timeLeft = 100;
                        Main.projectile[index6].timeLeft = 100;
                    }
                }
            }

            if (AttackTimer == 550 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (DifficultySystem.hellMode)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<SpiritBomb>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<SpiritBomb>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<SpiritBomb>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<SpiritBomb>(), NPC.damage / 4, 0f);
                }
                else
                {
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(8f, 8f), ModContent.ProjectileType<SpiritBomb2>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<SpiritBomb2>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(8f, -8f), ModContent.ProjectileType<SpiritBomb2>(), NPC.damage / 4, 0f);
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<SpiritBomb2>(), NPC.damage / 4, 0f);

                }
            }

            if (AttackTimer > 600)
                AttackTimer = 0;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadNPC(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/Trinity/SoulEffigy").Value;

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
