using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
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

        static int aiCryotaShotRateMax = 12;
        int aiCryotaShotRate = aiCryotaShotRateMax;

        Vector2 CircleDirc = new Vector2(0.0f, 15f);

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
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
            NPC.BossBar = ModContent.GetInstance<BossBars.Cryota>();
            NPC.npcSlots = 6;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
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

            if (!NPC.AnyNPCs(ModContent.NPCType<CryotasSentry>()))
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
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.LostSoulHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.LostSoulHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].timeLeft = 250;
                        Main.projectile[index6].timeLeft = 250;
                    }
                }
            }

            if (AttackTimer == 300)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.IceSpike, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

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

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.LostSoulHostile, NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 450 && AttackTimer < 800)
            {
                NPC.velocity = new Vector2(0f, 0f);

                aiCryotaShotRate--;

                if (aiCryotaShotRate <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, -0.10000000149011612, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.FrostBlastHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.FrostBlastHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].timeLeft = 250;
                        Main.projectile[index6].timeLeft = 250;
                    }
                }
            }

            if (AttackTimer > 810)
                AttackTimer = 0;
        }

        private void AI_Cryota_Attacks_Phase2()
        {
            // TODO: Cryota Phase 2 Attacks
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
