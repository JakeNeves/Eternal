using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.NPCs.Boss;

namespace Eternal.NPCs.Boss
{
    [AutoloadBossHead]
    class TeloticSeed : ModNPC
    {
        private Player player;
        private float speed;

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2048;
            npc.damage = 5;
            npc.defense = 5;
            npc.knockBackResist = 50f;
            npc.width = 64;
            npc.height = 64;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath2;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/UnfatefulStrike");
        }

        public override void AI()
        {
            Target();

            Move(new Vector2(0, -100f));

            if (npc.life == ~1000)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TeloticCrystalCluster"));
            }

            if (npc.life == ~500)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TeloticCrystal"));
            }

            if (npc.life == ~100)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("CrackedTeloticCrystal"));
            }

            npc.ai[1] -= 1f;
            if (npc.ai[1] <= 0f)
            {
                if (npc.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest(true);
                    npc.ai[0] = 1f;
                }
                if (npc.ai[1] != 3f && npc.ai[1] != 2f)
                {
                    npc.ai[1] = 2f;
                }
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                    {
                        npc.ai[1] = 3f;
                    }
                }
                if (npc.ai[1] == 2f)
                {
                    npc.rotation += npc.direction * 0.03f;
                    if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 250)
                    {
                        npc.velocity += Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * new Vector2(3.75f, 1.75f);
                    }

                    npc.velocity *= 0.98f;
                    npc.velocity.X = Utils.Clamp(npc.velocity.X, -4, 4);
                    npc.velocity.Y = Utils.Clamp(npc.velocity.Y, -2, 2);
                }
                else if (npc.ai[1] == 3f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.95f;
                    }
                    npc.velocity.X = npc.velocity.X * 0.95f;
                    if (npc.timeLeft > 50)
                    {
                        npc.timeLeft = 50;
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 20;
            int frame = (int)(npc.frameCounter / 1.0);
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;
            npc.frame.Y = frame * frameHeight;
        }

        private void Move(Vector2 offset)
        {
            speed = 5f;
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

        private void Target()
        {
            player = Main.player[npc.target];
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
