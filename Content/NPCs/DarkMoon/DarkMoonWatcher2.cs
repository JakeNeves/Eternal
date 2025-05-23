﻿using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.DarkMoon
{
    public class DarkMoonWatcher2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DemonEye];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 24;
            NPC.damage = 6;
            NPC.defense = 4;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 50f;
            NPC.aiStyle = 2;
            AIType = NPCID.DemonEye;
            AnimationType = NPCID.DemonEye;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.DarkMoon>().Type };
        }

        public override void AI()
        {
            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, 1.50f, 0.25f, 1.50f);

                if (Main.rand.NextBool(2))
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0) {
                var entitySource = NPC.GetSource_Death();

                int gore1 = Mod.Find<ModGore>("CarminiteInfestedEyeFront").Type;
                int gore2 = Mod.Find<ModGore>("CarminiteInfestedEyeBack").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
            }

            for (int k = 0; k < 5.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.OcculticMatter>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EventSystem.darkMoon)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.05f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
            
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, minimumDropped: 0, maximumDropped: 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OcculticMatter>(), minimumDropped: 2, maximumDropped: 6));
        }
    }
}
