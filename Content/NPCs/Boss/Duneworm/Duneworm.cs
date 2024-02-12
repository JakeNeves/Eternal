using Eternal.Common.Configurations;
using Eternal.Common.Misc;
using Eternal.Common.Systems;
using Eternal.Content.BossBarStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Duneworm
{
    [AutoloadBossHead]
	internal class DunewormHead : Duneworm
	{
		public override void SetStaticDefaults()
		{
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "Eternal/Content/NPCs/Boss/Duneworm/Duneworm_Preview",
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
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 64000;
			NPC.defense = 20;
			NPC.damage = 80;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			NPC.boss = true;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			if (Main.masterMode)
			{
				NPC.lifeMax = 256000;
				NPC.damage = 100;
				NPC.defense = 40;

			}
			else if (DifficultySystem.hellMode)
			{
				NPC.lifeMax = 512000;
				NPC.damage = 110;
				NPC.defense = 50;
			}
			else
			{
				NPC.lifeMax = 128000;
				NPC.damage = 90;
				NPC.defense = 30;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
        {
			potionType = ItemID.GreaterHealingPotion;
        }

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			// NPC loot will be added later :P
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

				new FlavorTextBestiaryInfoElement("A monsterous sandworm that feasts on dried husks and antlions, however this blood-thirsty creature also feats upon flesh! Beware of this sandworm...")
			});
		}

		public override void CustomBehavior()
		{
            if (ClientConfig.instance.bossBarExtras)
            {
                if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                {
                    EternalBossBarOverlay.SetTracked("Ambusher of the Desert", NPC);
                    EternalBossBarOverlay.visible = true;
                }
            }

            Player target = Main.player[NPC.target];

			NPC.TargetClosest(true);

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

			if (!target.ZoneDesert)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
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

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life < 0)
            {
				SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
			}
			else
            {
				SoundEngine.PlaySound(SoundID.DD2_BetsyHurt, NPC.position);

				for (int k = 0; k < 10.0; k++)
				{
					Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
				}
			}
        }
    }

	internal class DunewormBody : Duneworm
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
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 32000;
			NPC.defense = 60;
			NPC.damage = 80;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			if (Main.masterMode)
			{
				NPC.lifeMax = 2560000;
				NPC.damage = 100;
				NPC.defense = 40;

			}
			else if (DifficultySystem.hellMode)
			{
				NPC.lifeMax = 5120000;
				NPC.damage = 110;
				NPC.defense = 50;
			}
			else
			{
				NPC.lifeMax = 1280000;
				NPC.damage = 90;
				NPC.defense = 30;
			}
		}

		public override void CustomBehavior()
		{
			Player target = Main.player[NPC.target];

			NPC.TargetClosest(true);

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

			if (!target.ZoneDesert)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life < 0)
			{
				SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
			}
			else
			{
				SoundEngine.PlaySound(SoundID.DD2_BetsyHurt, NPC.position);

				for (int k = 0; k < 10.0; k++)
				{
					Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
				}
			}
		}
	}

	internal class DunewormTail : Duneworm
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
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath5;
			NPC.lifeMax = 32000;
			NPC.defense = 60;
			NPC.damage = 80;
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			if (Main.masterMode)
			{
				NPC.lifeMax = 2560000;
				NPC.damage = 100;
				NPC.defense = 40;

			}
			else if (DifficultySystem.hellMode)
			{
				NPC.lifeMax = 5120000;
				NPC.damage = 110;
				NPC.defense = 50;
			}
			else
			{
				NPC.lifeMax = 1280000;
				NPC.damage = 90;
				NPC.defense = 30;
			}
		}

		public override void CustomBehavior()
		{
			Player target = Main.player[NPC.target];

			NPC.TargetClosest(true);

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

			if (!target.ZoneDesert)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
		}

		public override void Init()
		{
			base.Init();
			tail = true;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life < 0)
			{
				SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
			}
			else
			{
				SoundEngine.PlaySound(SoundID.DD2_BetsyHurt, NPC.position);

				for (int k = 0; k < 10.0; k++)
				{
					Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
				}
			}
		}
	}

	public abstract class Duneworm : Worm
    {
		public override void Init()
		{
			minLength = 60;
			maxLength = 60;
			tailType = ModContent.NPCType<DunewormTail>();
			bodyType = ModContent.NPCType<DunewormBody>();
			headType = ModContent.NPCType<DunewormHead>();
			speed = 8f;
			turnSpeed = 0.05f;
			flies = false;
		}
	}
}
