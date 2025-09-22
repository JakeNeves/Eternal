using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Eternal.Common.Configurations;

namespace Eternal.Content.NPCs.Carrion
{
    public class PustuleSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 32;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 20;
            NPC.defense = 15;
            NPC.lifeMax = 300;
            AnimationType = NPCID.BlueSlime;
            SpawnModBiomes = [ModContent.GetInstance<Biomes.CarrionSurface>().Type, ModContent.GetInstance<Biomes.UndergroundCarrion>().Type];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 10.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Puss>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("Like slimes, they aren't very intelligent, but they are simply nothing but pus!")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneCarrion)
                return SpawnCondition.OverworldDay.Chance * 0.5f + SpawnCondition.Cavern.Chance * 0f;
            else if (ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion)
                return SpawnCondition.OverworldDay.Chance * 0f + SpawnCondition.Cavern.Chance * 0.5f;
            else
                return SpawnCondition.OverworldDay.Chance * 0f + SpawnCondition.Cavern.Chance * 0f;
        }
    }
}
