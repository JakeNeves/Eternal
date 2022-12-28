using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
	internal class StarbeltSeekerHead : StarbeltSeeker
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starbelt Seeker");


			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Content/NPCs/Comet/StarbeltSeeker_Preview",
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
			NPC.HitSound = SoundID.DD2_SkeletonHurt;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 2200;
			NPC.defense = 10;
			NPC.damage = 20;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int[] cometTileArray = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

			float baseChance = SpawnCondition.Overworld.Chance;
			float multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.5f : 0.75f;

			if (ModContent.GetInstance<ZoneSystem>().zoneComet)
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
			PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

			npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<StarmetalBar>(), 2, 12, 24));
			npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<GalaxianPlating>(), 2, 12, 24));
			npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 2, 12, 24));
			npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarSingularity>(), 2, 12, 24));

			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Starspear>(), 3));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornMask>(), 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHelmet>(), 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHat>(), 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHeadgear>(), 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornScalePlate>(), 12));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornGreaves>(), 12));
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("These otherworldly serpents like to burrow through comets and meteors, they are however very nimble navigator despite their lack in length.")
			});
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

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

	internal class StarbeltSeekerBody : StarbeltSeeker
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starbelt Seeker");

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
			NPC.HitSound = SoundID.DD2_SkeletonHurt;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 3200;
			NPC.defense = 10;
			NPC.damage = 20;
		}

        public override void CustomBehavior()
        {
			Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);
		}
	}

	internal class StarbeltSeekerTail : StarbeltSeeker
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starbelt Seeker");

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
			NPC.HitSound = SoundID.DD2_SkeletonHurt;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 3200;
			NPC.defense = 10;
			NPC.damage = 20;
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	public abstract class StarbeltSeeker : Worm
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starbelt Seeker");
		}

		public override void Init()
		{
			minLength = 8;
			maxLength = 12;
			tailType = ModContent.NPCType<StarbeltSeekerTail>();
			bodyType = ModContent.NPCType<StarbeltSeekerBody>();
			headType = ModContent.NPCType<StarbeltSeekerHead>();
			speed = 20f;
			turnSpeed = 0.15f;
			flies = true;
		}
	}
}
