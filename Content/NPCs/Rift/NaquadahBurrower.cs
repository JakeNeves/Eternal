using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
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
				CustomTexturePath = "Eternal/Content/NPCs/Comet/NaquadahBurrower_Preview",
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
			NPC.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
			NPC.DeathSound = SoundID.NPCDeath44;
			NPC.lifeMax = 96000;
			NPC.defense = 20;
			NPC.damage = 40;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.UndergroundRift>().Type };
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (RiftSystem.isRiftOpen && DownedBossSystem.downedArkofImperious)
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

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
            NPC.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.lifeMax = 96000;
			NPC.defense = 20;
			NPC.damage = 40;
        }

        public override void CustomBehavior()
        {
			Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
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
            NPC.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
            NPC.DeathSound = SoundID.NPCDeath44;
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
	}

	public abstract class NaquadahBurrower : Worm
    {
		public override void Init()
		{
			minLength = 20;
			maxLength = 40;
			tailType = ModContent.NPCType<NaquadahBurrowerTail>();
			bodyType = ModContent.NPCType<NaquadahBurrowerBody>();
			headType = ModContent.NPCType<NaquadahBurrowerHead>();
			speed = 20f;
			turnSpeed = 0.15f;
		}
	}
}
