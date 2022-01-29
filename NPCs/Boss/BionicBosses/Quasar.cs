using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Items.Potions;

namespace Eternal.NPCs.Boss.BionicBosses
{
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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ExoMenace");
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

        public override bool PreAI()
        {
            npc.netUpdate = true;
            npc.TargetClosest(true);
            if (npc.life < npc.lifeMax / 2)
            {
                Move(new Vector2(-0f, -200f));
            }
            else
            {
                Move(new Vector2(0f, 400f));
            }
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
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

        private void Move(Vector2 offset)
        {
            Player player = Main.player[npc.target];
            speed = 40.5f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
