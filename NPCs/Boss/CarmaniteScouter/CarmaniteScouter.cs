using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Items.BossBags;
using Eternal.Items.Accessories.Hell;

namespace Eternal.NPCs.Boss.CarmaniteScouter
{
    [AutoloadBossHead]
    class CarmaniteScouter : ModNPC
    {
        
        private Player player;
        private float speed;
        int Timer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 6000;
            npc.damage = 5;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 133;
            npc.height = 102;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CarmaniteScouterHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CarmaniteScouterDeath");
            music = MusicID.Boss5;
            bossBag = ItemType<CarmaniteScouterBag>();
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterParasite"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterChunk"), 1f);
            }
            else
            {

                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, 226, hitDirection, -2f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, 27, hitDirection, -1f, 0, default(Color), 0.7f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 9000; //(int)(npc.lifeMax + 30f * bossLifeScale);
            npc.damage = (int)(npc.damage + 1.5f);
            npc.defense = (int)(npc.defense + numPlayers);

            if(EternalWorld.hellMode)
            {
                npc.lifeMax = 10000; //(int)(npc.lifeMax + 30f * bossLifeScale);
                npc.damage = (int)(npc.damage + 2.5f);
                npc.defense = (int)(npc.defense + numPlayers);
            }
        }

        public override void AI()
        {

            if (NPC.AnyNPCs(NPCType<ScouterEye>()))
            {
                npc.dontTakeDamage = true;
            }
            if (!NPC.AnyNPCs(NPCType<ScouterEye>()))
            {
                npc.dontTakeDamage = false;
            }
            
            if (!Main.dayTime) { 
                Target();
            }

            Timer++;
            RotateNPCToTarget();
            DespawnHandler();

            if (Timer >= 0)
            {
                Move(new Vector2(0, -100f));
            }
            if (Timer == 100)
            {
                SpawnScouterEye();
            }
            if (Timer == 200)
            {
                Shoot();
            }
            if (Timer == 300)
            {
                SpawnScouterEye();
            }
            if (Timer == 400)
            {
                Shoot();
            }
            if (Timer == 500)
            {
                Timer = 0;
            }

            /*npc.ai[1] -= 1f;
            if(npc.ai[1] <= 0f)
            {
                Shoot();
            }*/
        }

        void SpawnScouterEye()
        {
            NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<ScouterEye>());
        }

        private void Shoot()
        {
            int type = mod.ProjectileType("CarmaniteParasite");
            Vector2 velocity = player.Center - npc.Center;
            float magnitude = Magnitude(velocity);
            if (magnitude > 0)
            {
                velocity *= 5f / magnitude;
            }
            else
            {
                velocity = new Vector2(0f, 5f);
            }
            Projectile.NewProjectile(npc.Center, velocity, type, npc.damage, 2f);
            npc.ai[1] = 25f;
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

        /* public override void FindFrame(int frameHeight)
         {
             npc.frameCounter += 1;
             npc.frameCounter %= 20;
             int frame = (int)(npc.frameCounter / 1.0);
             if (frame >= Main.npcFrameCount[npc.type]) frame = 0;
             npc.frame.Y = frame * frameHeight;

             RotateNPCToTarget();
         }*/

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        private void Target()
        {
            player = Main.player[npc.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead || Main.dayTime)
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

        public override void NPCLoot()
        {
            if(EternalWorld.hellMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<HeartofRage>());
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TritalodiumBar"), 30);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Carmanite"), 25);
            }

            EternalWorld.downedCarmaniteScouter = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
