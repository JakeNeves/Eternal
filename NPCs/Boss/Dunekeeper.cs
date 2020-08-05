using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss
{
    [AutoloadBossHead]
    class Dunekeeper : ModNPC
    {
        private Player player;
        private float speed;

        const float RotationSpeed = 0.5f;

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.damage = 5;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 58;
            npc.height = 58;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            bossBag = ItemType<DunekeeperBag>();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 60; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, 6, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, 6, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, 6, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
                }
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperLeftHalf"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperRightHalf"), 1f);
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    player.QuickSpawnItem(ItemType<Items.Weapons.Wasteland>());
                }

                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemType<Items.Weapons.StormBeholder>());
                }

                if (Main.rand.Next(3) == 0)
                {
                    player.QuickSpawnItem(ItemType<Items.Armor.ThunderduneHeadgear>());
                }

                player.QuickSpawnItem(ItemType<AncientDust>(), Main.rand.Next(25, 75));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1200; //(int)(npc.lifeMax + 5f * bossLifeScale);
            npc.damage = (int)(npc.damage + 1f);
            npc.defense = (int)(npc.defense + numPlayers);
        }

        public override void AI()
        {
            Target();

            DespawnHandler();

            if(npc.life <= 100)
            {
                DerpOut(new Vector2(0, -100));
                npc.ai[1] -= 1f;
                if (npc.ai[1] <= 0f)
                {
                }
            }
            else
            {
                Move(new Vector2(0, -100));
            }

            npc.ai[1] -= 1f;
            if (npc.ai[1] <= 0f)
            {


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

        

        private void Move(Vector2 offset)
        {
            speed = 5.75f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 0f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;

        }

        private void Warn()
        {
            Main.NewText("The Dunekeeper is Derping Out...", 0, 215, 215);
        }

        private void DerpOut(Vector2 offset)
        {
            speed = 50f;
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

            //Warn();
        }

        private void Target()
        {
            player = Main.player[npc.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
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
    }
}
