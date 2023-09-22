using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class NaquadahHead : ModNPC
    {
        private Player player;

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 20;
            NPC.damage = 60;
            NPC.defense = 36;
            NPC.lifeMax = 96000;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHitNaquadah")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.value = Item.sellPrice(gold: 30);
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 5;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

                new FlavorTextBestiaryInfoElement("Formed in the depths of outer space upon opening the rift, they roam the airspace usually close down to earth!")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (RiftSystem.isRiftOpen && DownedBossSystem.downedRiftArkofImperious && !Main.dayTime)
                return SpawnCondition.Sky.Chance * 1.75f;
            else
                return SpawnCondition.Sky.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawNaquadah>(), 1, 3, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalizedOminite>(), 2, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LargeRawNaquadah>(), 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RodofDistortion>(), 6));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("NaquadahHeadBody").Type;

            if (NPC.life <= 0)
            {
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
