using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class HexedSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 40;
            NPC.defense = 12;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 12, gold: 4);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            Banner = Item.NPCtoBanner(NPCID.Skeleton);
            BannerItem = Item.BannerToItem(Banner);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type, ModContent.GetInstance<Biomes.Mausoleum>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Remains of what was left of dead occultists, brought back to life!")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
                return SpawnCondition.OverworldNightMonster.Chance * 0.5f + SpawnCondition.Underground.Chance * 0f;
            else if (ModContent.GetInstance<ZoneSystem>().zoneMausoleum)
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underground.Chance * 0.25f;
            else
                return SpawnCondition.OverworldNightMonster.Chance * 0f + SpawnCondition.Underground.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 4, maximumDropped: 8));
        }
    }
}
