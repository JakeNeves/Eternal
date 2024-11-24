using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
    public class SearchRoller : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 28;
            NPC.aiStyle = 26;
            NPC.damage = 90;
            NPC.defense = 15;
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 2200;
            if (RiftSystem.isRiftOpen)
            {
                NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CometCreatureHitRift")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                    MaxInstances = 0,
                };
                NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CometCreatureDeathRift");
            }
            else
            {
                NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CometCreatureHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                    MaxInstances = 0,
                };
                NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/CometCreatureDeath");
            }
            NPC.value = Item.sellPrice(gold: 30);
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ModContent.GetInstance<ZoneSystem>().zoneComet)
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A compact lifeform detector that was used to search for otherworldly lifeforms, created by Dr. Sebastion Kox, a cosmic spirit possessed the roller entirely")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<StarmetalBar>(), 3, 6, 12));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<GalaxianPlating>(), 3, 6, 12));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 3, 6, 12));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarScrapMetal>(), 3, 6, 12));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VividMilkyWayClimax>(), 4));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarquartzCrystalCluster>(), 6, 1, 5));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornMask>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHelmet>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHat>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHeadgear>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornScalePlate>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornGreaves>(), 12));
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            NPC.rotation += NPC.velocity.X * 0.1f;

            if (RiftSystem.isRiftOpen)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0, -2f, 0, default, 1f);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("SearchRollerCore").Type;
            int gore2 = Mod.Find<ModGore>("SearchRollerShard").Type;

            if (NPC.life <= 0)
            {
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                for (int i = 0; i < 4; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
            }
            else
            {
                for (int k = 0; k < 0.50; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 2.5f, -2.5f, 0, default, 1.7f);
            }
        }
    }
}
