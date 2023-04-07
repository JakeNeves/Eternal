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

namespace Eternal.Content.NPCs.Tundra
{
    public class GlacerWalker : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glacer Walker");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoblinWarrior];
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 50;
            NPC.damage = 95;
            NPC.defense = 70;
            NPC.lifeMax = 888;
            NPC.value = Item.sellPrice(gold: 3);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.GoblinWarrior;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Shatter;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

                new FlavorTextBestiaryInfoElement("They roam the tundra, seeking treasure and hoarding it for themselves.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life < 0)
            {
                int gore1 = Mod.Find<ModGore>("GlacerWalkerHead").Type;
                int gore2 = Mod.Find<ModGore>("GlacerWalkerBody").Type;
                int gore3 = Mod.Find<ModGore>("GlacerWalkerArm").Type;
                int gore4 = Mod.Find<ModGore>("GlacerWalkerLeg").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore4);
            }
            else
            {
                for (int k = 0; k < 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Ice, 0, 0, 0, default(Color), 1f);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] snowTileArray = { TileID.SnowBlock, TileID.IceBlock, ModContent.TileType<GalaciteOre>() };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = snowTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.10f : 1f;

            Player player = Main.player[Main.myPlayer];

            if (DownedBossSystem.downedCosmicApparition && player.ZoneSnow)
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

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Items.Placeable.GalaciteOre>(), 1, 6, 8));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlacerWalkerHead>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlacerWalkerChestplate>(), 4, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlacerWalkerGreaves>(), 4, 1, 1));
        }
    }
}
