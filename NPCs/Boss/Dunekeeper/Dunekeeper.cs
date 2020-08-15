using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    [AutoloadBossHead]
    class Dunekeeper : ModNPC
    {
        private Player player;
        private float speed;

        int Timer;

        const int ShootType = ProjectileID.CultistBossLightningOrb;
        const int ShootType2 = ProjectileID.GreenLaser;
        const int ShootDamage = 5;
        const float ShootKnockback = 0f;
        const int ShootDirection = 5;

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.damage = 5;
            npc.defense = 15;
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
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 10000;
            }
        }

        public override void AI()
        {
            Target();
            Timer++;
            DespawnHandler();

            /*if (NPC.AnyNPCs(NPCType<DunekeeperHandL>()) && NPC.AnyNPCs(NPCType<DunekeeperHandR>()))
            {
                npc.dontTakeDamage = true;
            }
            if (!NPC.AnyNPCs(NPCType<DunekeeperHandL>()) && !NPC.AnyNPCs(NPCType<DunekeeperHandR>()))
            {
                npc.dontTakeDamage = false;
            }*/

            if (Timer >= 0)
            {
                Move(new Vector2(-100, 0));
            }
            if (Timer == 25 && EternalWorld.hellMode)
            {
                DunekeeperLightningOrb();
            }
            if (Timer == 50)
            {
                DunekeeperLightningLaser();
            }
            if (Timer == 100 && npc.life <= 6050)
            {
                DunekeeperLightningOrb();
            }
            if (Timer == 200)
            {
                Timer = 0;
            }
        }

        void DunekeeperLightningOrb()
        {
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, ShootDirection, 0, ShootType, ShootDamage, ShootKnockback, Main.myPlayer, 0f, 0f);
        }
        void DunekeeperLightningLaser()
        {
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, ShootDirection, 0, ShootType2, ShootDamage, ShootKnockback, Main.myPlayer, 0f, 0f);
        }

        private void Move(Vector2 offset)
        {
            if (EternalWorld.hellMode)
            {
                speed = 10.5f;
            }
            else
            {
                speed = 9.25f;
            }
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

        /*private void Warn()
        {
            Main.NewText("The Dunekeeper is Derping Out...", 0, 215, 215);
        }*/

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
            if (!player.active || player.dead || !player.ZoneDesert)
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
