using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items;
using Eternal.Tiles;
using System.Linq;
using Eternal.Items.Weapons.Magic;

namespace Eternal.NPCs.Comet
{
    class Sightseer : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 31;
            npc.height = 47;
            npc.damage = 100;
            npc.defense = 50;
            npc.lifeMax = 11000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.value = 50f;
            npc.aiStyle = 5;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
            }
        }

        private static int[] SpawnTiles = { };

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);
            npc.spriteDirection = npc.direction;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<RainmakerStave>());
            }
            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<InterstellarSingularity>(), Main.rand.Next(5, 20));
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<StarmetalBar>(), Main.rand.Next(10, 75));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<GalaxianPlating>(), Main.rand.Next(3, 12));
        }

    }
}
