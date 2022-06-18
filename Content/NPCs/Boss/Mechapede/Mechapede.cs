using Eternal.Content.Projectiles.Boss;
using Eternal.Content.Items.Potions;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.NPCs.Boss.Mechapede
{
	//[AutoloadBossHead]
	internal class MechapedeHead : Mechapede
	{
		public static int attackTimer;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Exo-Vlitch Mechapede");

			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Assets/Textures/Bestiary/Mechapede_Preview",
				PortraitPositionXOverride = 0f,
				PortraitPositionYOverride = 12f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);

			NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerHead);
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.lifeMax = 640000;
			NPC.defense = 45;
			NPC.width = 106;
			NPC.height = 68;
			NPC.boss = true;
			NPC.damage = 60;
			Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/MechanicalEchosofTheVlitch");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			if (Main.masterMode)
			{
				NPC.lifeMax = 2560000;
				NPC.damage = 240;
				NPC.defense = 55;

			}
			else if (DifficultySystem.hellMode)
			{
				NPC.lifeMax = 5120000;
				NPC.damage = 360;
				NPC.defense = 60;
			}
			else
			{
				NPC.lifeMax = 1280000;
				NPC.damage = 120;
				NPC.defense = 50;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
        {
			potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				new FlavorTextBestiaryInfoElement("A giant stolen war machine, capable of burrowing through the toughest terrain")
			});
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), minimumDropped: 16, maximumDropped: 20));
		}

        public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 1.30f, 0.30f, 0.42f);

			Player target = Main.player[NPC.target];

			/*if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (attackCounter > 0)
				{
					attackCounter--;
				}

				if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
				{
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

					var entitySource = NPC.GetSource_FromAI();
					var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ProjectileID.DeathLaser, NPC.damage, 0, Main.myPlayer);

					projectile.timeLeft = 300;

					attackCounter = 500;
					NPC.netUpdate = true;
				}
			}*/

			if (!target.active || target.dead)
			{
				NPC.TargetClosest(false);
				target = Main.player[NPC.target];
				if (!target.active || target.dead)
				{
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
					}
					return;
				}
			}
		}

		public override bool CheckActive()
		{
			Player player = Main.player[NPC.target];
			return !player.active || player.dead;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
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

	internal class MechapedeBody : Mechapede
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Exo-Vlitch Mechapede");

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
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.lifeMax = 640000;
			NPC.defense = 45;
			NPC.width = 106;
			NPC.height = 68;
			NPC.damage = 60;
		}

        public override void CustomBehavior()
        {
			var entitySource = NPC.GetSource_FromAI();

			Lighting.AddLight(NPC.position, 1.30f, 0.30f, 0.42f);
		}
	}

	internal class MechapedeTail : Mechapede
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Exo-Vlitch Mechapede");

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
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.lifeMax = 640000;
			NPC.defense = 45;
			NPC.width = 106;
			NPC.height = 68;
			NPC.damage = 60;
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 1.30f, 0.30f, 0.42f);
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	public abstract class Mechapede : Worm
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Exo-Vlitch Mechapede");
		}

		public override void Init()
		{
			minLength = 16;
			maxLength = 16;
			tailType = ModContent.NPCType<MechapedeTail>();
			bodyType = ModContent.NPCType<MechapedeBody>();
			headType = ModContent.NPCType<MechapedeHead>();
			speed = 25f;
			turnSpeed = 0.25f;
		}
	}
}
