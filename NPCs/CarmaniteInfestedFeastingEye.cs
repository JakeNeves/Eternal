using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;

namespace Eternal.NPCs
{
    class CarmaniteInfestedFeastingEye : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.WanderingEye];
        }

        public override void SetDefaults()
        {
            npc.width = 31;
            npc.height = 44;
            npc.damage = 2;
            npc.defense = 2;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 50f;
            npc.aiStyle = 2;
            aiType = NPCID.WanderingEye;
            animationType = NPCID.WanderingEye;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return Main.hardMode;
        }

    }
}
