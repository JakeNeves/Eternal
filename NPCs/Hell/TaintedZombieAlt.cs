using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Hell
{
    public class TaintedZombieAlt : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tainted Zombie");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.ArmedZombie];
		}

		public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;
			npc.damage = 36;
			npc.defense = 12;
			npc.lifeMax = 450;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = Item.sellPrice(gold: 1, silver: 3);
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			aiType = NPCID.ArmedZombie;
			animationType = NPCID.ArmedZombie;
			banner = Item.NPCtoBanner(NPCID.ArmedZombie);
			bannerItem = Item.BannerToItem(banner);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// Making sure that the Tainted Zombie ONLY spawn in Hell Mode
			if (EternalWorld.hellMode && EternalWorld.downedCarmaniteScouter)
				return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
			else
				return SpawnCondition.OverworldNightMonster.Chance * 0f;
		}

	}
}
