using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Corruption
{
    public class Teratoma : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 26;
            NPC.lifeMax = 240;
            NPC.defense = 20;
            NPC.damage = 15;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCDeath13;
            NPC.DeathSound = SoundID.NPCDeath22;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,

                new FlavorTextBestiaryInfoElement("These disgusting abominations have been floating somewhere in the Corruption")
            });
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("Teratoma1").Type;
            int gore2 = Mod.Find<ModGore>("Teratoma2").Type;
            int gore3 = Mod.Find<ModGore>("Teratoma3").Type;

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore2);
            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore3);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GreenBlood);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NPC.downedMoonlord)
                return SpawnCondition.Corruption.Chance * 0f;
            else
                return SpawnCondition.Corruption.Chance * 0.3f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<DeadMeat>(), 4, 2, 6));
        }
    }
}
