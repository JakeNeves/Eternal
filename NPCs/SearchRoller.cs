using Eternal.Items;
using Eternal.Items.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Tiles;
using System.Linq;
using static Terraria.ModLoader.ModContent;

namespace Eternal.NPCs
{
    public class SearchRoller : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 36;
            npc.aiStyle = 26;
            npc.damage = 90;
            npc.defense = 75;
            npc.knockBackResist = 0f;
            npc.lifeMax = 18000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath5;
            npc.value = Item.sellPrice(gold: 30);
        }

        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);
            npc.rotation += npc.velocity.X * 0.1f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<VividMilkyWayClimax>());
            }
            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<InterstellarSingularity>(), Main.rand.Next(5, 20));
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<StarmetalBar>(), Main.rand.Next(10, 75));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Astragel>(), Main.rand.Next(10, 50));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<GalaxianPlating>(), Main.rand.Next(3, 12));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerCore"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerShell"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerShell"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerShell"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerShell"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SearchRollerSpike2"), 1f);
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                    Dust.NewDust(npc.position, npc.width, npc.height, 27, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
            }
        }

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

    }
}
