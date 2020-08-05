using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.NPCs
{
    class GoldSlime : ModNPC
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
			npc.lifeMax = 4096;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 25f;
			npc.rarity = 3;
			aiType = NPCID.DungeonSlime;
			animationType = NPCID.DungeonSlime;
			Main.npcFrameCount[npc.type] = 2;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance * 0.15f;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Diamond, 15);
			}
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Amber, 20);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Ruby, 10);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Topaz, 10);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Emerald, 10);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Amethyst, 10);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, 10);
		}
	}
}
