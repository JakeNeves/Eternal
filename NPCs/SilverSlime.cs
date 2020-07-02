using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eternal.NPCs
{
    class SilverSlime : ModNPC
    {
        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.IceSlime];
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 23;
			npc.aiStyle = 1; 
			npc.defense = 50;
			npc.lifeMax = 2048;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 25f;
			npc.rarity = 3;
			aiType = NPCID.GreenSlime;
			animationType = NPCID.GreenSlime;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance * 0.25f;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Diamond, 3);
			}
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Amber, 10);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Ruby, 5);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Topaz, 5);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Emerald, 3);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Amethyst, 3);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, 5);
		}
	}
}
