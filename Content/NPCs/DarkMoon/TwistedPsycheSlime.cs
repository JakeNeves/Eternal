using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class TwistedPsycheSlime : ModNPC
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
            NPC.damage = 24;
            NPC.defense = 16;
            NPC.lifeMax = 120;
            AnimationType = NPCID.BlueSlime;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void AI()
        {
            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, 1.50f, 0.25f, 1.50f);

                for (int k = 0; k < 5.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0, 0, 0, default(Color), 1f);
                }
            }
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
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("When the glow of the darkened moon, shines upon the horizon, helpless slimes become psychebound by occultic energy! These however, are brutish...")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 6, maximumDropped: 10));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.25f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
            
        }
    }
}
