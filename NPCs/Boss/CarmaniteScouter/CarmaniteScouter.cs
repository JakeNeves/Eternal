using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.BossBags;
using Eternal.Items.Accessories.Hell;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Materials;
using Eternal.Projectiles.Boss;

namespace Eternal.NPCs.Boss.CarmaniteScouter
{
    [AutoloadBossHead]
    public class CarmaniteScouter : ModNPC
    {
        
        private Player player;
        private float speed;
        int Timer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 6000;
            npc.damage = 15;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 132;
            npc.height = 120;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CarmaniteScouterHit");
            npc.DeathSound = SoundID.NPCDeath12; //mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CarmaniteScouterDeath");
            music = MusicID.Boss5;
            bossBag = ModContent.ItemType<CarmaniteScouterBag>();
            if (EternalWorld.hellMode)
                npc.scale = 0.75f;
            else
                npc.scale = 1f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CarmaniteScouterParasite"), 1f);
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
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Blood, hitDirection, -2f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Blood, hitDirection, -1f, 0, default(Color), 0.7f);
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
            npc.damage = 16;
            npc.defense = 14;

            if(EternalWorld.hellMode)
            {
                npc.lifeMax = 10000; //(int)(npc.lifeMax + 30f * bossLifeScale);
                npc.damage = 20; //(int)(npc.damage + 2.5f);
                //npc.defense = (int)(npc.defense + numPlayers);
            }
        }

        public override void AI()
        {

            if (NPC.AnyNPCs(ModContent.NPCType<ScouterEye>()))
            {
                npc.dontTakeDamage = true;
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<ScouterEye>()))
            {
                npc.dontTakeDamage = false;
            }

            Timer++;
            Target();
            RotateNPCToTarget();
            DespawnHandler();

            if (Timer >= 0)
            {
                Move(new Vector2(0, 0f));
            }
            if (Timer == 100)
            {
                SpawnScouterEye();
            }
            if (Timer == 200 || Timer == 225 || Timer == 250 || Timer == 275)
            {
                Shoot();
            }
            if (Timer == 300)
            {
                SpawnScouterEye();
            }
            if (Timer == 400 || Timer == 425 || Timer == 450 || Timer == 475)
            {
                Shoot();
            }
            if (Timer == 500)
            {
                Timer = 0;
            }
        }

        void SpawnScouterEye()
        {
            //Main.PlaySound(new LegacySoundStyle(4, 13), Main.myPlayer);
            if (!NPC.AnyNPCs(ModContent.NPCType<ScouterEye>()))
            {
                NPC.NewNPC((int)npc.Center.X - 0, (int)npc.Center.Y, ModContent.NPCType<ScouterEye>());
            }
        }

        private void Shoot()
        {
            int type = ModContent.ProjectileType<CarmaniteParasite>();
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
            if (Timer == 130 || Timer == 230 || Timer == 330 || Timer == 430)
            {
                speed = 8f;
            }
            else
            {
                speed = 4f;
            }
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

        public override void NPCLoot()
        {
            //Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/AltBossDefeat"), Main.myPlayer);

            if (EternalWorld.hellMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HeartofRage>());
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarmaniteBane>());
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarmanitePurgatory>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarmaniteRipperClaws>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BruteCleavage>());
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Carmanite>(), Main.rand.Next(15, 30));
            }

            if (!EternalWorld.downedCarmaniteScouter)
            {
                //Main.NewText("The ground has been smothered with mysterious energy...", 215, 215, 0);
                Main.NewText("Something stirs in the darkest place beneath the world...", 224, 28, 7);
                EternalWorld.downedCarmaniteScouter = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
