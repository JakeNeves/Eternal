using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
	internal class NaquadahBurrowerHead : NaquadahBurrower
    {
		public override void SetStaticDefaults()
		{
			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Content/NPCs/Rift/NaquadahBurrower_Preview",
				Position = new Vector2(0f, 24f),
				PortraitPositionXOverride = 0f,
				PortraitPositionYOverride = 12f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerHead);
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHitNaquadah")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 96000;
			NPC.defense = 20;
			NPC.damage = 40;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (RiftSystem.isRiftOpen && DownedBossSystem.downedRiftArkofImperious)
			{
				return SpawnCondition.Underground.Chance * 1.5f;
			}
			else
			{
				return SpawnCondition.Underground.Chance * 0f;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawNaquadah>(), 1, 3, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalizedOminite>(), 2, 1, 2));
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				new FlavorTextBestiaryInfoElement("A powerful Ominite-bossed digger armored in pure Naquadah.")
			});
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);

			Player target = Main.player[NPC.target];

			NPC.TargetClosest(true);
		}

		public override bool CheckActive()
		{
			Player player = Main.player[NPC.target];
			return !player.active || player.dead;
		}

		public override void Init()
		{
			base.Init();
			head = true;
		}

		private int attackCounter;
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attackCounter = reader.ReadInt32();
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("NaquadahBurrowerHead").Type;

            if (NPC.life <= 0)
            {
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Wraith, 0, -1f, 0, default(Color), 1f);
            }
        }
    }

	internal class NaquadahBurrowerBody : NaquadahBurrower
    {
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerBody);
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHitNaquadah")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 96000;
			NPC.defense = 20;
			NPC.damage = 40;
        }

        public override void CustomBehavior()
        {
			Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("NaquadahBurrowerBody").Type;

            if (NPC.life <= 0)
            {
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Wraith, 0, -1f, 0, default(Color), 1f);
            }
        }
    }

	internal class NaquadahBurrowerTail : NaquadahBurrower
    {
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerTail);
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHitNaquadah")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 96000;
			NPC.defense = 20;
			NPC.damage = 40;
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("NaquadahBurrowerTail").Type;

            if (NPC.life <= 0)
            {
                 Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Wraith, 0, -1f, 0, default(Color), 1f);
            }
        }
    }

	public abstract class NaquadahBurrower : Worm
    {
		public override void Init()
		{
			minLength = 20;
			maxLength = 25;
			tailType = ModContent.NPCType<NaquadahBurrowerTail>();
			bodyType = ModContent.NPCType<NaquadahBurrowerBody>();
			headType = ModContent.NPCType<NaquadahBurrowerHead>();
			speed = 20f;
			turnSpeed = 0.15f;
		}
	}
}
