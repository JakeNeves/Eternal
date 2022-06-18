using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Igneopede
{
	[AutoloadBossHead]
	internal class IgneopedeHead : Igneopede
	{
		public override string Texture => "Eternal/Content/NPCs/Boss/Igneopede/IgneopedeHead";

		public static int attackTimer;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Igneopede");

			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Content/NPCs/Boss/Igneopede/Igneopede_Preview",
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
			NPC.HitSound = null;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 160000;
			NPC.defense = 10;
			NPC.width = 58;
			NPC.height = 76;
			NPC.boss = true;
			NPC.damage = 15;
			Music = MusicID.Boss3;
		}

		public override void OnKill()
		{
			NPC.SetEventFlagCleared(ref DownedBossSystem.downedIgneopede, -1);
		}

		public override void BossLoot(ref string name, ref int potionType)
        {
			potionType = ItemID.GreaterHealingPotion;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				new FlavorTextBestiaryInfoElement("The gigapede of the underworld, despite is impresive length, it's a nimble navigator.")
			});
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

			attackTimer++;

			Player target = Main.player[NPC.target];

			if (Main.netMode != NetmodeID.MultiplayerClient)
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
					var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ProjectileID.InfernoHostileBolt, NPC.damage, 0, Main.myPlayer);

					projectile.timeLeft = 300;

					attackCounter = 500;
					NPC.netUpdate = true;
				}

				if (attackTimer == 10 || attackTimer == 20 || attackTimer == 30 || attackTimer == 40)
				{
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

					var entitySource = NPC.GetSource_FromAI();
					var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ProjectileID.HeatRay, NPC.damage, 0, Main.myPlayer);

					projectile.timeLeft = 300;
					projectile.friendly = false;
					projectile.hostile = true;
				}
				if (attackTimer == 300)
                {
					attackTimer = 0;
                }

				if (!target.ZoneUnderworldHeight)
                {
					NPC.dontTakeDamage = true;
                }
				else
                {
					NPC.dontTakeDamage = false;
                }
			}

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

		public override void HitEffect(int hitDirection, double damage)
		{
			SoundEngine.PlaySound(SoundID.Tink, NPC.position);

			if (NPC.life <= 0)
            {
				var entitySource = NPC.GetSource_Death();

				int gore = Mod.Find<ModGore>("IgneopedeHeadGore").Type;

				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore);
			}
		}
	}

	internal class IgneopedeBody : Igneopede
	{
		public override string Texture => "Eternal/Content/NPCs/Boss/Igneopede/IgneopedeBody";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Igneopede");

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
			NPC.HitSound = null;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 160000;
			NPC.defense = 15;
			NPC.width = 58;
			NPC.height = 76;
			NPC.damage = 15;
		}

        public override void CustomBehavior()
        {
			var entitySource = NPC.GetSource_FromAI();

			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

			if (IgneopedeHead.attackTimer == 15 || IgneopedeHead.attackTimer == 25 || IgneopedeHead.attackTimer == 35 || IgneopedeHead.attackTimer == 45)
            {
				Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<IgneopedeFire>(), NPC.damage, 1, Main.myPlayer, 0, 0);
			}
		}

        public override void HitEffect(int hitDirection, double damage)
		{
			SoundEngine.PlaySound(SoundID.Tink, NPC.position);

			if (NPC.life <= 0)
			{
				var entitySource = NPC.GetSource_Death();

				int gore = Mod.Find<ModGore>("IgneopedeBodyGore").Type;

				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore);
			}
		}
	}

	internal class IgneopedeTail : Igneopede
	{
		public override string Texture => "Eternal/Content/NPCs/Boss/Igneopede/IgneopedeTail";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Igneopede");

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
			NPC.HitSound = null;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 160000;
			NPC.defense = 20;
			NPC.width = 58;
			NPC.height = 76;
			NPC.damage = 15;
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			SoundEngine.PlaySound(SoundID.Tink, NPC.position);

			if (NPC.life <= 0)
			{
				var entitySource = NPC.GetSource_Death();

				int gore = Mod.Find<ModGore>("IgneopedeTailGore").Type;

				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore);
			}
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	public abstract class Igneopede : Worm
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Igneopede");
		}

		public override void Init()
		{
			minLength = 12;
			maxLength = 12;
			tailType = ModContent.NPCType<IgneopedeTail>();
			bodyType = ModContent.NPCType<IgneopedeBody>();
			headType = ModContent.NPCType<IgneopedeHead>();
			speed = 10f;
			turnSpeed = 0.15f;
		}
	}
}
