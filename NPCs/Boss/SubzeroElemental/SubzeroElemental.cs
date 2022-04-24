using Eternal.Items.BossBags;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Placeable;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Radiant;
using Eternal.Items.Weapons.Summon;
using Eternal.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.SubzeroElemental
{
    [AutoloadBossHead]
    public class SubzeroElemental : ModNPC
    {

        #region Fundimentals
        private Player player;
        int AttackTimer = 0;
        int Phase = 0;
        float speed;
        #endregion

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 66;
            npc.height = 88;
            if (NPC.downedMoonlord)
            {
                npc.lifeMax = 92000;
                npc.damage = 100;
            }
            else
            {
                npc.lifeMax = 46000;
                npc.damage = 50;
            }
            npc.boss = true;
            npc.HitSound = null; //SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.aiStyle = -1;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            bossBag = ModContent.ItemType<SubzeroElementalBag>();
            music = MusicID.Boss3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (NPC.downedMoonlord)
            {
                npc.lifeMax = 134000;
                npc.damage = 200;
            }
            else
            {
                npc.lifeMax = 92000;
                npc.damage = 100;
            }

            if (EternalWorld.hellMode)
            {
                if (NPC.downedMoonlord)
                {
                    npc.lifeMax = 176000;
                    npc.damage = 250;
                }
                else
                {
                    npc.lifeMax = 134000;
                    npc.damage = 200;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalHead1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalHead2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalBody1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalBody2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalLeftArm"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SubzeroElementalRightArm"), 1f);
            }
            else
            {
                Main.PlaySound(SoundID.Tink, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Ice);
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Chilled, 180, false);
            if (EternalWorld.hellMode)
            {
                player.AddBuff(BuffID.Weak, 180, false);
            }
        }

        private void Shoot()
        {
            int type = ModContent.ProjectileType<FridgedSpike>();
            Vector2 velocity = player.Center - npc.Center;
            float magnitude = Magnitude(velocity);
            if (magnitude > 0)
            {
                velocity *= 8f / magnitude;
            }
            else
            {
                velocity = new Vector2(0f, 10f);
            }
            Projectile.NewProjectile(npc.Center, velocity, type, npc.damage, 2f);
        }

        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.232f, 0.196f, 0.84f);

            player = Main.player[npc.target];

            Move(new Vector2(0f, 0f));

            DespawnHandler();

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }

            if (!player.ZoneSnow || NPC.AnyNPCs(ModContent.NPCType<SubzeroElementalShield>()))
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            AttackTimer++;

            switch (AttackTimer)
            {
                case 110 | 120 | 130:
                    Shoot();
                    break;
                case 140 | 180 | 190:
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCID.IceElemental);
                    if (EternalWorld.hellMode)
                    {
                        NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCID.SpikedIceSlime);
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCID.IceSlime);
                    }
                    break;
                case 200 | 210 | 220:
                    Shoot();
                    break;
                case 240 | 260 | 280:
                    if (!NPC.AnyNPCs(ModContent.NPCType<SubzeroElementalShield>()))
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<SubzeroElementalShield>());
                    }
                    if (NPC.downedMoonlord)
                    {
                        Vector2 direction = Main.player[npc.target].Center - npc.Center;
                        direction.Normalize();
                        direction.X *= 8.5f;
                        direction.Y *= 8.5f;

                        int amountOfProjectiles = Main.rand.Next(8, 24);
                        for (int i = 0; i < amountOfProjectiles; ++i)
                        {
                            float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                            float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                            int damage = Main.expertMode ? 15 : 17;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<FridgedSpike>(), npc.damage, 1, Main.myPlayer, 0, 0);
                        }
                    }
                    break;
                case 300:
                    AttackTimer = 0;
                    break;
            }

        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void Move(Vector2 offset)
        {
            if (!player.ZoneSnow)
            {
                npc.rotation = npc.velocity.X * 0.08f;
                speed = 24f;
            }
            else if (Phase == 1)
            {
                npc.rotation = npc.velocity.X * 0.08f;
                speed = 12f;
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.06f;
                speed = 10f;
            }
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 2f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 2f);
            magnitude = Magnitude(move);
            npc.spriteDirection = npc.direction = npc.Center.X < player.Center.X ? -1 : 1;
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
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
            if (!EternalWorld.downedSubzeroElemental)
            {
                Main.NewText("The air is getting fridged in the tundra...", 0, 95, 215);
                //Main.NewText("Someone is reconizing your devotion into preventing the underworld from freezing...", 0, 80, 200);
                EternalWorld.downedSubzeroElemental = true;
            }
            else if (NPC.downedMoonlord && EternalWorld.downedSubzeroElemental && !EternalWorld.downedSubzeroElementalP2)
            {
                Main.NewText("Ancient Ice Constructs of the Tundra have been empowered...", 0, 95, 215);
                EternalWorld.downedSubzeroElementalP2 = true;
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostblightCrystal>(), Main.rand.Next(10, 20));

                if (Main.rand.Next(1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostGladiator>());
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostyImmaterializer>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheKelvinator>());
                }

                if (NPC.downedMoonlord)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SydaniteOre>(), Main.rand.Next(15, 55));

                    if (Main.rand.Next(4) == 0)
                    {
                        player.QuickSpawnItem(ModContent.ItemType<Frostpike>());
                    }

                    if (Main.rand.Next(5) == 0)
                    {
                        player.QuickSpawnItem(ModContent.ItemType<FrostDisk>());
                    }

                }
            }

        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (NPC.downedMoonlord)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = ItemID.GreaterHealingPotion;
            }

            name = "The " + name;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
