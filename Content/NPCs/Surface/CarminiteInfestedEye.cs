using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Surface
{
    public class CarminiteInfestedEye : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DemonEye];
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 24;
            NPC.damage = 2;
            NPC.defense = 2;
            NPC.lifeMax = 15;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath12;
            NPC.value = 50f;
            NPC.aiStyle = 2;
            AIType = NPCID.DemonEye;
            AnimationType = NPCID.DemonEye;
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("CarminiteInfestedEyeFront").Type;
            int gore2 = Mod.Find<ModGore>("CarminiteInfestedEyeBack").Type;

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A Demon Eye that unfortunatey got infected by a cometite sample along with a large chunk of flesh, overtime the eye itself is now completely infested.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carminite>(), minimumDropped: 1, maximumDropped: 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, minimumDropped: 0, maximumDropped: 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Poutine>(), 12));
        }
    }
}
