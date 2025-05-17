using Eternal.Common.Configurations;
using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Hell;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Pets;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Niades
{
    [AutoloadBossHead]
    public class Niades : ModNPC
    {
        public static LocalizedText NiadesDefeated { get; private set; }

        int teleportTimer;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            NiadesDefeated = Mod.GetLocalization($"BossDefeatedEvent.{nameof(NiadesDefeated)}");

            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        ref float AttackTimer => ref NPC.ai[1];

        int aiNiadesShootTime = 6;
        int aiNiadesShootRate = 12;

        float aiNiadesProjectileRotation = MathHelper.PiOver2;

        public override void SetDefaults()
        {
            NPC.width = 134;
            NPC.height = 134;
            NPC.aiStyle = -1;
            NPC.damage = 25;
            NPC.defense = 30;
            NPC.lifeMax = 120000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.boss = true;
            NPC.npcSlots = 6;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/Tenebricide");
            SpawnModBiomes = [ModContent.GetInstance<Biomes.DarkMoon>().Type];
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedNiades, -1);

            if (!DownedBossSystem.downedNiades)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(NiadesDefeated.Value, 200, 50, 200);
                else if (Main.netMode == NetmodeID.Server)
                    ChatHelper.BroadcastChatMessage(NiadesDefeated.ToNetworkText(), new Color(200, 50, 200));
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {

                new FlavorTextBestiaryInfoElement("A psychic construct brought to life by twisted occultism, presumably it's somewhat unstable!")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("NiadesOuter").Type;
                int gore2 = Mod.Find<ModGore>("NiadesRing").Type;

                for (int i = 0; i < 4; i++)
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
            }

            for (int k = 0; k < 15.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, 0, 0, default(Color), 1f);
            }
        }

        public override bool PreAI()
        {
            Player player = Main.player[NPC.target];

            NPC.rotation = NPC.velocity.X * 0.02f;

            float speed = 15f;
            float acceleration = 0.05f;
            if (Main.expertMode)
            {
                speed = 30f;
                acceleration = 0.10f;
            }
            else if (DifficultySystem.hellMode)
            {
                speed = 45f;
                acceleration = 0.15f;
            }
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

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Vector2 circDir = new Vector2(0f, 45f);

            var entitySource = NPC.GetSource_FromAI();

            teleportTimer++;
            AttackTimer++;

            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 2.15f, 0.95f, 2.15f);

            if (NPC.life < NPC.lifeMax / 2 || DifficultySystem.hellMode)
            {
                AI_Niades_Attacks_Phase2();
            }
            else {
                AI_Niades_Attacks_Phase1();
            }

            if (DifficultySystem.hellMode)
            {
                if (teleportTimer >= 300)
                {
                    NPC.position = new Vector2(player.position.X + Main.rand.NextFloat(-200f, 200f), player.position.Y + Main.rand.NextFloat(-200f, 200f));

                    for (int i = 0; i < 15; i++) {
                        circDir = Utils.RotatedBy(circDir, 0.45, new Vector2());
                        int projectile = Projectile.NewProjectile(entitySource, NPC.Center, circDir, ProjectileID.InsanityShadowHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    }

                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    teleportTimer = 0;
                }
            }
            else if (Main.expertMode)
            {
                if (teleportTimer >= 450)
                {
                    NPC.position = new Vector2(player.position.X + Main.rand.NextFloat(-200f, 200f), player.position.Y + Main.rand.NextFloat(-200f, 200f));

                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    teleportTimer = 0;
                }
            }
            else
            {
                if (teleportTimer >= 600)
                {
                    NPC.position = new Vector2(player.position.X + Main.rand.NextFloat(-200f, 200f), player.position.Y + Main.rand.NextFloat(-200f, 200f));

                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                    teleportTimer = 0;
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            if (!player.active || player.dead || !EventSystem.darkMoon) {
                NPC.velocity.Y -= 0.15f;
                NPC.EncourageDespawn(10);
                return;
            }
        }

        private void AI_Niades_Attacks_Phase1()
        {
            Player player = Main.player[NPC.target];

            Vector2 circDir = new Vector2(0f, 45f);

            var entitySource = NPC.GetSource_FromAI();

            AttackTimer++;

            if (!EventSystem.darkMoon)
                AttackTimer = 0;

            if (AttackTimer >= 300 && AttackTimer < 700)
            {
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(8))
                    {
                        SoundEngine.PlaySound(SoundID.Item122, NPC.position);

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                        
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = true;
                        Main.projectile[proj].tileCollide = false;
                    }
                }
            }

            if (AttackTimer >= 500 && AttackTimer < 800)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiNiadesProjectileRotation += 0.01f;
                    if (--aiNiadesShootTime <= 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item103, NPC.position);

                        aiNiadesShootTime = aiNiadesShootRate;
                        var shootPos = NPC.Center + new Vector2(0, 15);
                        var shootVel = new Vector2(0, 5).RotatedBy(aiNiadesProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.ShadowBeamHostile, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ProjectileID.ShadowBeamHostile, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(MathHelper.Pi), ProjectileID.ShadowBeamHostile, NPC.damage, 1f),
                            Projectile.NewProjectile(entitySource, shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ProjectileID.ShadowBeamHostile, NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                        }
                    }
                }
            }

            if (AttackTimer >= 900 && AttackTimer < 1200)
            {
                if (Main.rand.NextBool(3))
                {
                    var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                    var shootVel = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-20f, -15f));
                    int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.Shadowflames, NPC.damage / 2 * ((Main.expertMode) ? 3 : 2), 1f);
                    Main.projectile[i].hostile = true;
                    Main.projectile[i].tileCollide = true;
                    Main.projectile[i].friendly = false;
                }
            }

            if (AttackTimer > 1300)
                AttackTimer = 0;
        }

        private void AI_Niades_Attacks_Phase2()
        {
            Player player = Main.player[NPC.target];

            Vector2 circDir = new Vector2(0f, 45f);

            var entitySource = NPC.GetSource_FromAI();

            AttackTimer++;

            if (!EventSystem.darkMoon)
                AttackTimer = 0;

            if (AttackTimer >= 300 && AttackTimer < 900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    aiNiadesProjectileRotation += 0.15f;
                    if (--aiNiadesShootTime <= 0)
                    {
                        aiNiadesShootTime = aiNiadesShootRate;
                        var shootPos = NPC.Center + new Vector2(0, 30);
                        var shootVel = new Vector2(0, 10).RotatedBy(aiNiadesProjectileRotation);
                        int[] i = [
                            Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.EyeLaser, NPC.damage, 1f)
                        ];
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].tileCollide = false;
                        }
                    }
                }
            }

            if (AttackTimer >= 1200 && AttackTimer < 1500)
            {
                if (Main.rand.NextBool(3))
                {
                    SoundEngine.PlaySound(SoundID.Item104, NPC.position);

                    var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                    var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-20f, -30f));
                    int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.InsanityShadowHostile, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                    Main.projectile[i].tileCollide = true;
                    Main.projectile[i].friendly = false;
                }

                if (Main.rand.NextBool(6))
                {
                    SoundEngine.PlaySound(SoundID.Item104, NPC.position);

                    var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                    var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(20f, 30f));
                    int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ProjectileID.InsanityShadowHostile, NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                    Main.projectile[i].tileCollide = true;
                    Main.projectile[i].friendly = false;
                }
            }

            if (AttackTimer >= 1600 && AttackTimer < 1900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.ShadowBeamHostile, NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].tileCollide = false;
                    Main.projectile[index5].timeLeft = 300;

                    if (DifficultySystem.hellMode)
                    {
                        if (Main.rand.NextBool(6))
                        {
                            var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), 1000);
                            var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-30f, -45f));
                            int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                            Main.projectile[i].tileCollide = true;
                            Main.projectile[i].friendly = false;
                        }

                        if (Main.rand.NextBool(9))
                        {
                            var shootPos = Main.player[NPC.target].position + new Vector2(Main.rand.Next(-1000, 1000), -1000);
                            var shootVel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(30f, 45f));
                            int i = Projectile.NewProjectile(entitySource, shootPos, shootVel, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 4 * ((Main.expertMode) ? 3 : 2), 1f);
                            Main.projectile[i].tileCollide = true;
                            Main.projectile[i].friendly = false;
                        }
                    }
                }
            }

            if (AttackTimer >= 2000 && AttackTimer < 2300)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), ProjectileID.EyeLaser, (int)(NPC.damage * 0.5f), 0f, Main.myPlayer);
                }
            }

            if (AttackTimer >= 2400 && AttackTimer < 2800)
            {
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.rand.NextBool(8))
                    {
                        SoundEngine.PlaySound(SoundID.Item122, NPC.position);

                        int proj = Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

                        Main.projectile[proj].tileCollide = false;
                    }

                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    int index6 = Projectile.NewProjectile(entitySource, NPC.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<Psyfireball>(), NPC.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].timeLeft = 250;
                    Main.projectile[index6].timeLeft = 250;
                }
            }

            if (AttackTimer > 2900)
                AttackTimer = 0;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // TODO: Niades Loot Table

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<NiadesBag>()));

            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<OrbofTheOccult>(), 4));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SactothsConquest>(), 12));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.30f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
