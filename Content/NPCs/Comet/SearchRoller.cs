using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Tiles;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
    public class SearchRoller : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 28;
            NPC.aiStyle = 26;
            NPC.damage = 90;
            NPC.defense = 15;
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 1100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.value = Item.sellPrice(gold: 30);
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] cometTileArray = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.15f : 0.95f;

            if (ModContent.GetInstance<ZoneSystem>().zoneComet)
            {
                return baseChance * multiplier;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<StarmetalBar>(), 2, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<GalaxianPlating>(), 2, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 2, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarSingularity>(), 2, 12, 24));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VividMilkyWayClimax>(), 3, 1, 1));
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);
            NPC.rotation += NPC.velocity.X * 0.1f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / NPC.lifeMax * 50; k++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }
    }
}
