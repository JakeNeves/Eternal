using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eternal.Content.NPCs.Rift
{
    public class ShiftspiralHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        int moveTimer;

        const float acceleration = 0.2f;
        const float speed = 8f;

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 36;

            NPC.lifeMax = 32000;
            NPC.damage = 60;
            NPC.defense = 16;
            NPC.knockBackResist = -1f;
            NPC.alpha = 100;

            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath52;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            NPC.spriteDirection = NPC.direction;

            if (!NPC.AnyNPCs(ModContent.NPCType<Shiftspiral>()))
            {
                NPC.active = false;
            }
            NPC parent = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<Shiftspiral>())
                {
                    parent = Main.npc[i];
                    break;
                }
            }
            if (Main.netMode != 1)
            {
                NPC.localAI[0] -= 1f;
                if (NPC.localAI[0] <= 0f)
                {
                    NPC.localAI[0] = (float)Main.rand.Next(120, 480);
                    NPC.ai[0] = (float)Main.rand.Next(-100, 101);
                    NPC.ai[1] = (float)Main.rand.Next(-100, 101);
                    NPC.netUpdate = true;
                }
            }
            NPC.TargetClosest(true);
            float speed = 0.3f;
            float num780 = 300f;
            if ((double)parent.life < (double)parent.lifeMax * 0.25)
            {
                num780 += 100f;
            }
            if ((double)parent.life < (double)parent.lifeMax * 0.1)
            {
                num780 += 100f;
            }
            if (!parent.active || ModContent.NPCType<Shiftspiral>() < 0)
            {
                NPC.active = false;
                return;
            }
            float targetX = parent.position.X + (float)(parent.width / 2);
            float targetY = parent.position.Y + (float)(parent.height / 2);
            Vector2 vector97 = new Vector2(targetX, targetY);
            float num784 = targetX + NPC.ai[0];
            float num785 = targetY + NPC.ai[1];
            float num786 = num784 - vector97.X;
            float num787 = num785 - vector97.Y;
            float num788 = (float)Math.Sqrt((double)(num786 * num786 + num787 * num787));
            num788 = num780 / num788;
            num786 *= num788;
            num787 *= num788;
            if (NPC.position.X < targetX + num786)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && num786 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            else if (NPC.position.X > targetX + num786)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && num786 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            if (NPC.position.Y < targetY + num787)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && num787 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            else if (NPC.position.Y > targetY + num787)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && num787 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            float maxSpeed = 12f;
            if (NPC.velocity.X > maxSpeed)
            {
                NPC.velocity.X = maxSpeed;
            }
            if (NPC.velocity.X < -maxSpeed)
            {
                NPC.velocity.X = -maxSpeed;
            }
            if (NPC.velocity.Y > maxSpeed)
            {
                NPC.velocity.Y = maxSpeed;
            }
            if (NPC.velocity.Y < -maxSpeed)
            {
                NPC.velocity.Y = -maxSpeed;
            }
            if (num786 > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2((double)num787, (double)num786);
            }
            if (num786 < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2((double)num787, (double)num786) + 3.14f;
                return;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
