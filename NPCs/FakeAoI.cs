using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace Eternal.NPCs
{
    class FakeAoI : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fake Ark of Imparus");
        }

        public override void SetDefaults()
        {
            npc.width = 110;
            npc.height = 110;
            npc.aiStyle = -1;
            npc.defense = 99;
            npc.lifeMax = 990000;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.Item4;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 99.9f;
            npc.damage = 100;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //The Fake Ark of Imparus is currently under testing right now...
            return 0f;
        }

        public override void AI()
        {
            if (npc.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                npc.ai[0] = 1f;
            }
            if (npc.ai[1] != 3f && npc.ai[1] != 2f)
            {
                Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
