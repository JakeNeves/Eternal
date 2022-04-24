using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs
{
    public class DesertSword : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 46;
            npc.height = 46;
            npc.damage = 90;
            npc.defense = 24;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = 50f;
            npc.aiStyle = 23;
            aiType = 23;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Sand, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneDesert && NPC.downedMoonlord)
                return SpawnCondition.OverworldDayDesert.Chance * 0.75f;
            else
                return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.20f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ThunderiteOre>(), Main.rand.Next(6, 36));
            }
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ThunderblightCrystal>(), Main.rand.Next(4, 8));
            }
        }

    }
}
