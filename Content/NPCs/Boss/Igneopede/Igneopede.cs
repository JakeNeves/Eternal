using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Igneopede
{
	internal class IgneopedeHead : Igneopede
	{
		public override string Texture => "Eternal/Content/NPCs/Boss/Igneopede/IgneopedeHead";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Igneopede");

			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Eternal/Content/NPCs/Boss/Igneopede/Igneopede_Preview",
				Position = new Vector2(40f, 24f),
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
			NPC.HitSound = null;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 32000;
			NPC.defense = 10;
			NPC.width = 58;
			NPC.height = 76;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			DisplayName.SetDefault("The Igneopede");

			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement("The gigapede of the underworld, it burrows through the toughest, yet hottest terrain known to man.")
			});
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (attackCounter > 0)
				{
					attackCounter--;
				}

				Player target = Main.player[NPC.target];

				if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
				{
					Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
					direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

					var entitySource = NPC.GetSource_FromAI();
					var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ProjectileID.HeatRay, 5, 0, Main.myPlayer);

					projectile.timeLeft = 300;
					projectile.friendly = false;
					projectile.hostile = true;

					attackCounter = 500;
					NPC.netUpdate = true;
				}
			}
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
			NPC.lifeMax = 32000;
			NPC.defense = 15;
			NPC.width = 58;
			NPC.height = 76;
		}

        public override void CustomBehavior()
        {
			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
		}

        public override void HitEffect(int hitDirection, double damage)
		{
			SoundEngine.PlaySound(SoundID.Tink, NPC.position);
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
			NPC.lifeMax = 32000;
			NPC.defense = 20;
			NPC.width = 58;
			NPC.height = 76;
		}

		public override void CustomBehavior()
		{
			Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			SoundEngine.PlaySound(SoundID.Tink, NPC.position);
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
			speed = 20f;
			turnSpeed = 0.030f;
		}
	}
}
