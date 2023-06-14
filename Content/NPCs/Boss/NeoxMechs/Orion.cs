using Eternal.Common.Systems;
using Eternal.Content.Items.Potions;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.NeoxMechs
{
    //[AutoloadBossHead]
    public class Orion : ModNPC
    {
        private Player player;

        int AttackTimer = 0;

        bool justSpawnedCircle = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orion N30X");
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                CombatText.NewText(NPC.Hitbox, Color.Red, "POWERING DOWN...", dramatic: true);
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Polarus>());
            }
            else
            {
                for (int k = 0; k < 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Electric, 0, -2f, 0, default(Color), 1f);
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void SetDefaults()
        {
            NPC.width = 142;
            NPC.height = 142;
            NPC.lifeMax = 2400000;
            NPC.defense = 75;
            NPC.damage = 60;
            NPC.aiStyle = -1;
            NPC.knockBackResist = -1f;
            NPC.value = Item.buyPrice(platinum: 6, gold: 40);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/NeoxPower");
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 4800000;
                NPC.damage = 120;
                NPC.defense = 60;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 9600000;
                NPC.damage = 140;
                NPC.defense = 75;
            }
            else if (DifficultySystem.sinstormMode)
            {
                NPC.lifeMax = 18000000;
                NPC.damage = 140;
                NPC.defense = 90;
            }
            else
            {
                NPC.lifeMax = 3600000;
                NPC.damage = 100;
                NPC.defense = 45;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            NPC.rotation = NPC.velocity.X * 0.01f;

            var entitySource = NPC.GetSource_FromAI();

            Lighting.AddLight(NPC.Center, 0.77f, 0.18f, 0.86f);

            if (!justSpawnedCircle)
            {
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<OrionNeoxCircle>(), NPC.damage / 3, 0, 0, 0f, NPC.whoAmI);
                justSpawnedCircle = true;
            }

            NPC.netUpdate = true;
            player = Main.player[NPC.target];
            NPC.dontTakeDamage = false;
            float speed = 26.75f;
            float acceleration = 0.15f;
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
            DespawnHandler();

            return true;
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        public override void AI()
        {
            AttackTimer++;
            DoAttacksPhase1();
        }

        private void DoAttacksPhase1()
        {
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;
            int amountOfProjectiles;

            if (Main.expertMode)
            {
                amountOfProjectiles = Main.rand.Next(4, 8);
            }
            else if (DifficultySystem.hellMode)
            {
                amountOfProjectiles = Main.rand.Next(8, 12);
            }
            else
            {
                amountOfProjectiles = Main.rand.Next(2, 4);
            }

            /*if (AttackTimer == 100 || AttackTimer == 105 || AttackTimer == 110 || AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125 || AttackTimer == 130)
            {
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OrionNeoxLaser>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }
            if (AttackTimer == 200 || AttackTimer == 205 || AttackTimer == 210 || AttackTimer == 215 || AttackTimer == 220 || AttackTimer == 225 || AttackTimer == 230)
            {
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-100, 100) * 0.01f;
                    float B = (float)Main.rand.Next(-100, 100) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Main.PlaySound(SoundID.DD2_KoboldExplosion, NPC.position);
                    Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OrionNeoxBomb>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }*/
            if (AttackTimer == 231)
            {
                AttackTimer = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
