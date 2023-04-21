using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
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
    public class CometSlime : ModNPC
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
            NPC.damage = 12;
            NPC.defense = 12;
            NPC.lifeMax = 20000;
            AnimationType = NPCID.BlueSlime;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.DemonTorch, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

                new FlavorTextBestiaryInfoElement("When the comet landed onto the world, the gelbound agglomarations infused the potential power of the stars within their bodies.")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CometiteOre>(), minimumDropped: 12, maximumDropped: 48));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedMoonlord)
                return SpawnCondition.Underground.Chance * 0.75f;
            else
                return SpawnCondition.Underground.Chance * 0f;
        }
    }
}
