using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Summon;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Tiles;
using System.Linq;

namespace Eternal.NPCs
{
    class CosmicImmaterializingSeekerofTheCosmicChampion : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Immaterializing Seeker of The Cosmic Champion");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DemonEye];
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 24;
            npc.damage = 2;
            npc.defense = 2;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.Item79;
            npc.value = 50f;
            npc.aiStyle = NPCID.DemonEye;
            aiType = NPCID.DemonEye;
            animationType = NPCID.DemonEye;
        }

        private static int[] SpawnTiles = { };

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && NPC.downedMoonlord && player.ZoneOverworldHeight ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
        }

        /*public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == TileType<CometiteOre>() ? 100f : 0f;
        } 

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return NPC.downedMoonlord = true;
        }*/

        public override void NPCLoot()
        {
            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<InterstellarSingularity>(), Main.rand.Next(5, 20));
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<StarmetalBar>(), Main.rand.Next(10, 75));
        }

    }
}
