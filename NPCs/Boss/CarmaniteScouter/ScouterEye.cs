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
            npc.width = 36;
            npc.height = 22;
            npc.damage = 2;
            npc.defense = 2;
            npc.lifeMax = 10;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noTileCollide = true;
            npc.aiStyle = 2;
            aiType = NPCID.DemonEye;
            animationType = NPCID.DemonEye;
        }
    }
}
