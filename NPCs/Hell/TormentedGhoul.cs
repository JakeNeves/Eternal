using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Hell
{
    public class TormentedGhoul : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tormented Ghoul");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DesertGhoul];
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.DesertGhoul);
			npc.width = 34;
			npc.height = 50;
			npc.damage = 36;
			npc.defense = 16;
			npc.lifeMax = 1000;
			npc.value = Item.sellPrice(gold: 1, silver: 3);
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = NPCID.DesertGhoul;
			animationType = NPCID.DesertGhoul;
			banner = Item.NPCtoBanner(NPCID.DesertGhoul);
			bannerItem = Item.BannerToItem(banner);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// Making sure that the Tainted Zombie ONLY spawn in Hell Mode
			if (EternalWorld.hellMode && Main.hardMode)
				return SpawnCondition.OverworldDayDesert.Chance * 0.5f;
			else
				return SpawnCondition.OverworldDayDesert.Chance * 0f;
		}

	}
}
