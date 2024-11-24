using Eternal.Common.Systems;
using Eternal.Content.Buffs;
using Eternal.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
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
			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				CustomTexturePath = "Eternal/Content/NPCs/Rift/RiftSerpent_Preview",
				Position = new Vector2(4f, 24f),
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
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 4400;
			NPC.defense = 5;
			NPC.damage = 20;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Rift>().Type };
			NPC.dontTakeDamage = true;
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<RiftWithering>(), 1 * 60 * 60, false);
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("RiftSerpentHead").Type;

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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoteofOminite>(), 1, 2, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShiftblightShard>(), 2, 2, 4));
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
			if (!Main.dedServ)
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
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
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
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 4400;
			NPC.defense = 5;
			NPC.damage = 20;
            NPC.dontTakeDamage = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<RiftWithering>(), 1 * 60 * 60, false);
        }

        public override void CustomBehavior()
        {
			if (!Main.dedServ)
				Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("RiftSerpentBody").Type;

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

	internal class RiftSerpentTail : RiftSerpent
    {
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
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
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/RiftEnemyHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCDeath/RiftEnemyDeath");
            NPC.lifeMax = 4400;
			NPC.defense = 10;
			NPC.damage = 20;
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<RiftWithering>(), 1 * 60 * 60, false);
        }

        public override void CustomBehavior()
		{
			if (!Main.dedServ)
				Lighting.AddLight(NPC.Center, 0.75f, 0f, 0.75f);
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("RiftSerpentTail").Type;

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

	public abstract class RiftSerpent : Worm
    {
		public override void Init()
		{
			if (Main.zenithWorld)
			{
                minLength = 48;
                maxLength = 48;
            }
			else
			{
                minLength = 24;
                maxLength = 24;
            }
			tailType = ModContent.NPCType<RiftSerpentTail>();
			bodyType = ModContent.NPCType<RiftSerpentBody>();
			headType = ModContent.NPCType<RiftSerpentHead>();
			speed = 35f;
			turnSpeed = 0.10f;
			flies = true;
		}
	}
}
