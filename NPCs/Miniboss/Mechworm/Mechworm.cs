using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Miniboss.Mechworm
{
    public abstract class Mechworm : Worm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechworm");
        }

        public override void Init()
        {
            minLen = 50;
            maxLen = 75;
            headType = ModContent.NPCType<MechwormHead>();
            bodyType = ModContent.NPCType<MechwormBody>();
            tailType = ModContent.NPCType<MechwormTail>();
            speed = 24f;
            turnSpeed = 0.050f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(1) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MechwormFang>());
            }
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HiTechHitbow>());
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }

        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { TileID.Mud, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && NPC.downedMoonlord && player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight ? 2.09f : 0f;
            }
            return SpawnCondition.Underground.Chance * 0.8f;
        }*/
    }
}
