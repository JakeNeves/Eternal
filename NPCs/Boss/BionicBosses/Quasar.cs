using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    [AutoloadBossHead]
    public class Quasar : ModNPC
    {

        private float speed;

        int attackTimer = 0;
        int frameNum;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XE-90 Quasar");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 620000;
            npc.width = 76;
            npc.height = 62;
            npc.damage = 160;
            npc.defense = 32;
            npc.knockBackResist = -1f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.boss = true;
            music = MusicID.Boss4;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1240000;
            npc.damage = 120;
            npc.defense = 60;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 2530000;
                npc.damage = 240;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "SYSTEM FAILIURES DETECTED, SELF-DESTRUCT INITIATED...", dramatic: true);
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Electric, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Fire, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override bool PreAI()
        {
            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
                npc.TargetClosest(false);
            float speed = 15.5f;
            float acceleration = 0.4f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
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
            if (npc.velocity.X < xDir)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if (npc.velocity.X < 0 && xDir > 0)
                    npc.velocity.X = npc.velocity.X + acceleration;
            }
            else if (npc.velocity.X > xDir)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.X > 0 && xDir < 0)
                    npc.velocity.X = npc.velocity.X - acceleration;
            }
            if (npc.velocity.Y < yDir)
            {
                npc.velocity.Y = npc.velocity.Y + acceleration;
                if (npc.velocity.Y < 0 && yDir > 0)
                    npc.velocity.Y = npc.velocity.Y + acceleration;
            }
            else if (npc.velocity.Y > yDir)
            {
                npc.velocity.Y = npc.velocity.Y - acceleration;
                if (npc.velocity.Y > 0 && yDir < 0)
                    npc.velocity.Y = npc.velocity.Y - acceleration;
            }

            return true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void AI()
        {
            RotateNPCToTarget();
            attackTimer++;
            if (npc.life < npc.lifeMax / 2)
            {
                if (attackTimer == 150 || attackTimer == 152 || attackTimer == 154 || attackTimer == 156 || attackTimer == 158 || attackTimer == 160 || attackTimer == 162 || attackTimer == 164)
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;
                    int amountOfProjectiles = 3;

                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-100, 100) * 0.01f;
                        float B = (float)Main.rand.Next(-100, 100) * 0.01f;
                        int damage = Main.expertMode ? 20 : 30;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeFire, npc.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
            }
            else
            {
                if (attackTimer == 150 || attackTimer == 155 || attackTimer == 160 || attackTimer == 165 || attackTimer == 350 || attackTimer == 355 || attackTimer == 360 || attackTimer == 365)
                {
                    Vector2 direction = Main.player[npc.target].Center - npc.Center;
                    direction.Normalize();
                    direction.X *= 8.5f;
                    direction.Y *= 8.5f;
                    int amountOfProjectiles = 3;

                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-100, 100) * 0.01f;
                        float B = (float)Main.rand.Next(-100, 100) * 0.01f;
                        int damage = Main.expertMode ? 20 : 30;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CursedFlameHostile, npc.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 250 || attackTimer == 260 || attackTimer == 270 || attackTimer == 280 || attackTimer == 450 || attackTimer == 455 || attackTimer == 460 || attackTimer == 465)
                {

                }
            }
            if (attackTimer == 470)
            {
                attackTimer = 0;
            }

            if (npc.life < npc.lifeMax / 2)
            {
                frameNum = 1;
            }
            else
            {
                frameNum = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameNum * frameHeight;
        }

        private void RotateNPCToTarget()
        {
            Player player = Main.player[npc.target];
            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<Photon>()) && !NPC.AnyNPCs(ModContent.NPCType<Proton>()))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SightCore>(), Main.rand.Next(20, 40));

            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WeatheredPlating>(), Main.rand.Next(3, 9));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
