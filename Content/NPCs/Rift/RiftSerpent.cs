using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
	internal class RiftSerpentHead : RiftSerpent
    {
		public override void SetStaticDefaults()
		{
			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Content/NPCs/Rift/RiftSerpent_Preview",
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
			NPC.HitSound = SoundID.DD2_MonkStaffGroundImpact;
			NPC.DeathSound = SoundID.NPCDeath44;
			NPC.lifeMax = 44000;
			NPC.defense = 10;
			NPC.damage = 20;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
			NPC.dontTakeDamage = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int[] cometTileArray = {TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

			float baseChance = SpawnCondition.Overworld.Chance;
			float multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.5f : 0.75f;

			if (RiftSystem.isRiftOpen)
			{
				return baseChance * multiplier;
			}
			else
			{
				return SpawnCondition.Overworld.Chance * 0f;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoteofOminite>(), 1, 2, 6));
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("A powerful entity who's weak area is it's shiny crystal on it's tail.")
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

	internal class RiftSerpentBody : RiftSerpent
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
            NPC.HitSound = SoundID.DD2_MonkStaffGroundImpact;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.lifeMax = 44000;
			NPC.defense = 10;
			NPC.damage = 20;
            NPC.dontTakeDamage = true;
        }

        public override void CustomBehavior()
        {
			Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
		}
	}

	internal class RiftSerpentTail : RiftSerpent
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
            NPC.HitSound = SoundID.DD2_MonkStaffGroundImpact;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.lifeMax = 44000;
			NPC.defense = 10;
			NPC.damage = 20;
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

	public abstract class RiftSerpent : Worm
    {
		public override void Init()
		{
			minLength = 24;
			maxLength = 96;
			tailType = ModContent.NPCType<RiftSerpentTail>();
			bodyType = ModContent.NPCType<RiftSerpentBody>();
			headType = ModContent.NPCType<RiftSerpentHead>();
			speed = 35f;
			turnSpeed = 0.10f;
			flies = true;
		}
	}
}
