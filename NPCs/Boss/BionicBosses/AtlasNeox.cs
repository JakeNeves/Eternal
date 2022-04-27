using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    //[AutoloadBossHead]
    public class AtlasNeox : ModNPC
    {
        private Player player;

        bool justSpawnedArms = false;
        bool phase2Init = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXR-2306 Atlas-N30X");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1240000;
            npc.damage = 80;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 252;
            npc.height = 244;
            npc.value = Item.buyPrice(platinum: 6, gold: 40);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = MusicID.LunarBoss;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            //bossBag = ItemType<CarmaniteScouterBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2530000;
            npc.damage = 120;
            npc.defense = 85;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6000000;
                npc.damage = 240;
                npc.defense = 90;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINE EXR-2303...", dramatic: true);
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void AI()
        {
            if (!justSpawnedArms)
            {
                if (npc.life < npc.lifeMax / 2)
                {
                    NPC.NewNPC((int)npc.position.X - 200, (int)npc.position.Y - 100, ModContent.NPCType<AtlasNeoxSaw>());
                    NPC.NewNPC((int)npc.position.X + 200, (int)npc.position.Y - 100, ModContent.NPCType<AtlasNeoxSlicers>());
                    NPC.NewNPC((int)npc.position.X - 100, (int)npc.position.Y, ModContent.NPCType<AtlasNeoxVice>());
                    NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, ModContent.NPCType<AtlasNeoxVice>());
                }
                else
                {
                    NPC.NewNPC((int)npc.position.X - 64, (int)npc.position.Y, ModContent.NPCType<AtlasNeoxLaser>());
                    NPC.NewNPC((int)npc.position.X - 64, (int)npc.position.Y, ModContent.NPCType<AtlasNeoxCannon>());
                }
                justSpawnedArms = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<AtlasNeoxLaser>()) || NPC.AnyNPCs(ModContent.NPCType<AtlasNeoxCannon>()) || NPC.AnyNPCs(ModContent.NPCType<AtlasNeoxSaw>()) || NPC.AnyNPCs(ModContent.NPCType<AtlasNeoxVice>()) || NPC.AnyNPCs(ModContent.NPCType<AtlasNeoxSlicers>()))
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            if (npc.life < npc.lifeMax / 2)
            {
                if (!phase2Init)
                {
                    CombatText.NewText(npc.Hitbox, Color.Red, "DEPLOYING MELEE MODULES...", dramatic: true);
                    justSpawnedArms = false;
                    phase2Init = true;
                }
            }
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 1.17f, 0.10f, 0.32f);

            npc.rotation = npc.velocity.X * 0.01f;

            npc.netUpdate = true;
            player = Main.player[npc.target];
            npc.dontTakeDamage = false;
            float speed = 36.25f;
            float acceleration = 0.4f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.05F;
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
            DespawnHandler();

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
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
            player = Main.player[npc.target];
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<BorealisNeox>());

            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NeoxCore>(), Main.rand.Next(6, 16));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
