using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.NPCs.Boss
{
    class ScorchSlime : ModNPC
    {
		public override void SetStaticDefaults()
		{
			//Main.npcFrameCount[npc.type] = 2;
		}

		public override void SetDefaults()
		{
			npc.width = 43;
			npc.damage = 3;
			npc.height = 31;
			npc.aiStyle = 1;
			npc.defense = 50;
			npc.lifeMax = 200;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 25f;
			npc.rarity = 3;
			npc.lavaImmune = true;
			aiType = NPCID.DungeonSlime;
			animationType = NPCID.DungeonSlime;
			Main.npcFrameCount[npc.type] = 2;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Electrified] = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Venom] = true;
		}
	}
}
