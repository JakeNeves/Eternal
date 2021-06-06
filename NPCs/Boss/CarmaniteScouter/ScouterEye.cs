using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Summon;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs.Boss.CarmaniteScouter
{
    class ScouterEye : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DemonEye];
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 20;
            npc.damage = 6;
            npc.defense = 2;
            npc.lifeMax = 80;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noTileCollide = true;
            npc.aiStyle = 2;
            aiType = NPCID.DemonEye;
            animationType = NPCID.DemonEye;
        }
    }
}
