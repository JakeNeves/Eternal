using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Beneath
{
    public class VoraciousSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Skeleton];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(silver: 12, gold: 4);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Skeleton;
            Banner = Item.NPCtoBanner(NPCID.Skeleton);
            BannerItem = Item.BannerToItem(Banner);
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Beneath>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("It's best you stay away from them, they will eat away at your body!")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && ModContent.GetInstance<ZoneSystem>().zoneBeneath)
                return SpawnCondition.Cavern.Chance * 0.15f;
            else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), minimumDropped: 2, maximumDropped: 6));
        }
    }
}
