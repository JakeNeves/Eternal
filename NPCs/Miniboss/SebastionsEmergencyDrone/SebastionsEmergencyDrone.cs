using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.NPCs.Miniboss.SebastionsEmergencyDrone
{
    public class SebastionsEmergencyDrone : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sebastion's Emergerncy Drone");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 29;
            npc.height = 33;
            npc.lifeMax = 56000;
            npc.defense = 10;
            npc.damage = 12;
            npc.aiStyle = 62;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            aiType = NPCID.ElfCopter;
            npc.boss = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "An Emergency Drone";
            potionType = ItemID.None;
        }

        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.20f, 0.30f, 0.40f);
            npc.spriteDirection = npc.direction;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.12f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

    }
}
