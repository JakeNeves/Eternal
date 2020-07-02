using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items;

namespace Eternal.NPCs.Boss
{

    class TeloticCrystalCluster : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("A Telotic Crystal");
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.aiStyle = 2;
			npc.defense = 50;
			npc.lifeMax = 4000;
			npc.HitSound = SoundID.Item4;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 25f;
			npc.rarity = 3;
			npc.boss = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/UnfatefulStrike");
			aiType = NPCID.SolarCorite;
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientDust"), 20);
		}
	}
}
