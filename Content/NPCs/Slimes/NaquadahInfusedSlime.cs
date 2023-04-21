using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Slimes
{
    public class NaquadahInfusedSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 64;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 36;
            NPC.defense = 20;
            NPC.lifeMax = 44000;
            AnimationType = NPCID.BlueSlime;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Wraith, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

                new FlavorTextBestiaryInfoElement("Upon opening the rift and the Ark of Imperious' defeat, the slimes absorbed the raw essence of Naquadah.")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawNaquadah>(), minimumDropped: 6, maximumDropped: 12));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (RiftSystem.isRiftOpen && DownedBossSystem.downedArkofImperious)
                return SpawnCondition.Underground.Chance * 0.5f;
            else
                return SpawnCondition.Underground.Chance * 0f;
        }
    }
}
