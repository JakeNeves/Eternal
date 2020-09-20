using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.BossBags;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    [AutoloadBossHead]
    public class Dunekeeper : ModNPC
    {
        private Player player;

        #region Fundimentals
        bool Spin = false;
        int SpeedValue = 0;
        int Phase = 0;
        int SpinTimer = 0;
        int AttackTimer = 0;
        #endregion

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
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
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
                    player.QuickSpawnItem(ItemType<Wasteland>());
                }

                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(ItemType<StormBeholder>());
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
                npc.lifeMax = 12000;
            }
        }

        void DunekeeperLasers()
        {
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -90, 0, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 90, 0, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, 90, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, -90, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
        }

        void DunekeeperDiagonalLasers()
        {
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, -45, 0, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 45, 0, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, 45, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 20, npc.position.Y + 20, 0, -45, ProjectileID.DesertDjinnCurse, 5, 0, Main.myPlayer, 0f, 0f);
        }

        public override void AI()
        {
            if (NPC.AnyNPCs(NPCType<DunekeeperHandL>()))
            {
                npc.dontTakeDamage = true;
            }
            if (!NPC.AnyNPCs(NPCType<DunekeeperHandL>()))
            {
                npc.dontTakeDamage = false;
            }
            if (NPC.AnyNPCs(NPCType<DunekeeperHandR>()))
            {
                npc.dontTakeDamage = true;
            }
            if (!NPC.AnyNPCs(NPCType<DunekeeperHandR>()))
            {
                npc.dontTakeDamage = false;
            }

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }

            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;

            Player player = Main.player[npc.target];

            SpeedValue = Spin ? 3 : 4;

            Vector2 moveTo = player.Center - npc.Center;
            moveTo.Normalize();
            moveTo = moveTo * SpeedValue;

            npc.velocity = moveTo;

            if (++SpinTimer % 180 == 0)
            {
                if (Spin)
                {
                    Spin = true;
                }
                else
                {
                    Spin = false;
                }
            }

            if(Spin)
            {
                npc.rotation += npc.velocity.X * 0.25f;
            }

            switch(Phase)
            {
                case 0:
                    if (++AttackTimer >= 225)
                    {
                        for (int i = 0; i < Main.rand.Next(5, 15); i++)
                        {
                            DunekeeperLasers();
                            if (EternalWorld.hellMode)
                            {
                                DunekeeperDiagonalLasers();
                            }
                        }
                    }
                    if (++AttackTimer >= 230)
                    {
                        for (int i = 0; i < Main.rand.Next(5, 15); i++)
                        {
                            DunekeeperDiagonalLasers();
                            if (EternalWorld.hellMode)
                            {
                                DunekeeperLasers();
                            }
                        }

                        AttackTimer = 0;
                    }
                    break;
                case 1:
                    if (++AttackTimer >= 225)
                    {
                        for (int i = 0; i < Main.rand.Next(5, 15); i++)
                        {
                            DunekeeperLasers();
                            DunekeeperDiagonalLasers();
                        }

                        AttackTimer = 0;
                    }
                    break;
            }

           /* if (Phase == 0)
            {
                if (++AttackTimer >= 225)
                {
                    for (int i = 0; i < Main.rand.Next(5, 15); i++)
                    {
                        DunekeeperLasers();
                        if (EternalWorld.hellMode)
                        {
                            DunekeeperDiagonalLasers();
                        }
                    }
                }
                if (++AttackTimer >= 230)
                {
                    for (int i = 0; i < Main.rand.Next(5, 15); i++)
                    {
                        DunekeeperDiagonalLasers();
                        if (EternalWorld.hellMode)
                        {
                            DunekeeperLasers();
                        }
                    }

                    AttackTimer = 0;
                }
            }
            else if (Phase == 0)
            {
                if (++AttackTimer >= 225)
                {
                    for (int i = 0; i < Main.rand.Next(5, 15); i++)
                    {
                        DunekeeperLasers();
                        DunekeeperDiagonalLasers();
                    }

                    AttackTimer = 0;
                }
            } */

        }
    }
}
