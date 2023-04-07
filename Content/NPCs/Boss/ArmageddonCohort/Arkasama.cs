using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Potions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.ArmageddonCohort
{
    public class Arkasama : ModNPC
    {
        bool phase2Init = false;
        bool takeActionInit = false;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);
        Vector2 vector32 = Vector2.Zero;

        int e2 = 300;
        int attackTimer = 0;

        float zeroradianstuff;

        public bool SpawnedShield
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }

        public bool TakeAction
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : 0f;
        }

        public ref float RemainingShields => ref NPC.localAI[2];

        public static int ShieldType()
        {
            return ModContent.NPCType<ArkasamaShield>();
        }

        public static int ShieldCount()
        {
            int count = 16;

            if (Main.expertMode)
            {
                count += 4;
            }
            else if (DifficultySystem.hellMode)
            {
                if (ModContent.GetInstance<ServerConfig>().BrutalHellMode)
                {
                    count += 12;
                }
                else
                {
                    count += 8;
                }
            }

            return count;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Armageddon Arkasama");

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.lifeMax = 1280000;
            NPC.defense = 40;
            NPC.width = 118;
            NPC.height = 324;
            NPC.boss = true;
            NPC.damage = 65;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            Music = MusicID.Boss3;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), minimumDropped: 16, maximumDropped: 20));
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 5120000;
                NPC.damage = 240;
                NPC.defense = 80;

            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 10240000;
                NPC.damage = 360;
                NPC.defense = 90;
            }
            else
            {
                NPC.lifeMax = 2560000;
                NPC.damage = 120;
                NPC.defense = 70;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A giant stolen mechanical sword, originally used to ward off the Ark of Imperious")
            });
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
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ArkasamaShield>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                if (minionNPC.ModNPC is ArkasamaShield minion)
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

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            SpawnShield();

            CheckShield();

            if (NPC.AnyNPCs(ModContent.NPCType<ArkasamaShield>()))
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

            if (TakeAction)
            {
                attackTimer++;

                if (!takeActionInit)
                {
                    Main.NewText("Protocol 30 Initiated, preparing to attack", 130, 18, 42);
                    takeActionInit = true;
                }

                #region movement
                float speed = 32f;
                float acceleration = 0.32f;
                Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

                NPC.TargetClosest(true);
                NPC.spriteDirection = NPC.direction;
                Player player = Main.player[NPC.target];
                if (player.dead || !player.active)
                {
                    NPC.TargetClosest(false);
                    NPC.active = false;
                }

                NPC.rotation = NPC.velocity.X * 0.03f;

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
                #endregion

                if (NPC.life < NPC.lifeMax / 2)
                {
                    if (!phase2Init)
                    {
                        Main.NewText("Protocol 26 Initiated, empowering attacks", 130, 18, 42);
                        phase2Init = true;
                    }

                    if (attackTimer > 100 && attackTimer < 200)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].tileCollide = false;
                        Main.projectile[index5].timeLeft = 300;
                        Main.projectile[index6].tileCollide = false;
                        Main.projectile[index6].timeLeft = 300;
                    }
                    if (attackTimer > 300 && attackTimer < 400)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.20000000298023224, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].tileCollide = false;
                        Main.projectile[index5].timeLeft = 300;
                        Main.projectile[index6].tileCollide = false;
                        Main.projectile[index6].timeLeft = 300;
                    }

                    if (attackTimer > 1000)
                    {
                        attackTimer = 0;
                    }
                }
                else
                {
                    if (attackTimer > 100 && attackTimer < 200)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ProjectileID.DeathLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].tileCollide = false;
                        Main.projectile[index5].timeLeft = 300;
                        Main.projectile[index6].tileCollide = false;
                        Main.projectile[index6].timeLeft = 300;
                    }

                    if (attackTimer > 500)
                    {
                        attackTimer = 0;
                    }
                }
            }
        }

        private void CheckShield()
        {
            if (TakeAction)
            {
                return;
            }

            float remainingShieldsSum = 0f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC otherNPC = Main.npc[i];
                if (otherNPC.active && otherNPC.type == ShieldType() && otherNPC.ModNPC is ArkasamaShield minion)
                {
                    if (minion.ParentIndex == NPC.whoAmI)
                    {
                        remainingShieldsSum += (float)otherNPC.life / otherNPC.lifeMax;
                    }
                }
            }

            RemainingShields = remainingShieldsSum / ShieldCount();

            if (RemainingShields <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                TakeAction = true;
                NPC.netUpdate = true;
            }
        }

        public override bool CheckActive()
        {
            Player player = Main.player[NPC.target];
            return !player.active || player.dead;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
