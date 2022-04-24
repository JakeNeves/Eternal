using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Placeable;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Hell
{
    public class ScorchingWarrior : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 72;
            npc.height = 86;
            npc.damage = 120;
            npc.defense = 60;
            npc.lifeMax = 48000;
            npc.HitSound = null;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = Item.sellPrice(gold: 6, silver: 10);
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Main.PlaySound(SoundID.Tink, npc.position);

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.BlueTorch, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            }
        }

        public override void AI()
        {
            npc.rotation = npc.velocity.X * 0.03f;

            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }

            float speed = 18f;
            float acceleration = 0.10f;
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
                    acceleration += 0.10F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.15F;
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
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneUnderworldHeight && NPC.downedMoonlord && EternalWorld.hellMode)
                return SpawnCondition.Underworld.Chance * 0.25f;
            else
                return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ScoriumOre>(), Main.rand.Next(6, 36));
            }
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InfernoblightCrystal>(), Main.rand.Next(4, 8));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
