using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Hell
{
    public class TaintedZombie : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tainted Zombie");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
		}

		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;
			npc.damage = 28;
			npc.defense = 6;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = Item.sellPrice(gold: 1, silver: 3);
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
			banner = Item.NPCtoBanner(NPCID.Zombie);
			bannerItem = Item.BannerToItem(banner);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// Making sure that the Tainted Zombie ONLY spawn in Hell Mode
			if (EternalWorld.hellMode)
				return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
			else
				return SpawnCondition.OverworldNightMonster.Chance * 0f;
		}

	}
}
