using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Miniboss
{
    public class StarbornInquisitorMelee : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starborn Inquisitor");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 100;
            NPC.defense = 80;
            NPC.lifeMax = 25000;
            NPC.value = Item.sellPrice(platinum: 6);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.PossessedArmor;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Shatter;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Perhaps, one of the strongest entities anyone has encountered, this one likes to use blunt force!")
            });
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
            }

            Dust dust;
            dust = Main.dust[Dust.NewDust(NPC.position, (int)NPC.width, (int)NPC.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / NPC.lifeMax * 20.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PurpleTorch, hitDirection, 0, 0, default(Color), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] cometTileArray = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier;

            if (DifficultySystem.hellMode)
            {
                multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.25f : 1f;
            }
            else
            {
                multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.1f : 1f;
            }

            if (DownedBossSystem.downedCosmicApparition && ModContent.GetInstance<ZoneSystem>().zoneComet)
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
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.CometiteOre>(), 6, 4, 8));
        }
    }
}
