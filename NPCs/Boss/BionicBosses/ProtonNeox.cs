using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Eternal.Items.Potions;

namespace Eternal.NPCs.Boss.BionicBosses
{
    public class ProtonNeox : ModNPC
    {
        bool phase2Init = false;

        private Player player;

        int attackTimer = 0;
        int frameNum;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXE-3076 Proton-N30X");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1240000;
            npc.width = 152;
            npc.height = 124;
            npc.damage = 160;
            npc.defense = 32;
            npc.knockBackResist = -1f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/NeoxPower");
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
            npc.lifeMax = 2530000;
            npc.damage = 120;
            npc.defense = 85;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6060000;
                npc.damage = 240;
                npc.defense = 90;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            npc.netUpdate = true;
            npc.TargetClosest(true);
            player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
            }
            return true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<PhotonNeox>()) && !NPC.AnyNPCs(ModContent.NPCType<QuasarNeox>()))
                    CombatText.NewText(npc.Hitbox, Color.Blue, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINE EXR-2350...", dramatic: true);
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

        public override void AI()
        {
            RotateNPCToTarget();

            float speed = 24f;
            float acceleration = 0.5f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.10F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.15F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.20F;
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

            if (npc.life < npc.lifeMax / 2)
            {
                frameNum = 1;
                if (!phase2Init)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                        Dust dust = Dust.NewDustPerfect(npc.position, DustID.BlueTorch);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Normalize(position - npc.Center) * 4;
                        dust.noLight = false;
                        dust.fadeIn = 1f;
                    }
                    phase2Init = true;
                }
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

        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<PhotonNeox>()) && !NPC.AnyNPCs(ModContent.NPCType<QuasarNeox>()))
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<PolarusNeox>());
        }

        private void RotateNPCToTarget()
        {
            player = Main.player[npc.target];
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
