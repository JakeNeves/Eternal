using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Beneath
{
	internal class PsywormHead : Psyworm
	{
        public static int attackTimer;

        public override void SetStaticDefaults()
		{
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "Eternal/Content/NPCs/Beneath/Psyworm_Preview",
                Position = new Vector2(16f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 8f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.width = 50;
            NPC.height = 76;
            NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 1000;
			NPC.defense = 10;
			NPC.damage = 20;
			NPC.value = Item.sellPrice(platinum: 3, gold: 30, silver: 60);
			SpawnModBiomes = [ ModContent.GetInstance<Biomes.Beneath>().Type ];
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
			{
                return SpawnCondition.Cavern.Chance * 0.05f;
			}
			else
			{
				return SpawnCondition.Cavern.Chance * 0f;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PsyblightEssence>(), 1, 2, 6));
        }

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange([
				new FlavorTextBestiaryInfoElement("A horrifying psychic monster that lurks within the dankest of caverns!")
			]);
		}

		public override void CustomBehavior()
		{
			Player target = Main.player[NPC.target];

			NPC.TargetClosest(true);

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
                if (attackCounter > 0)
                {
                    attackCounter--;
                }

                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/Psy")
                    {
                        Volume = 0.8f,
                        PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                        MaxInstances = 0,
                    }, NPC.Center);

                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

                    var entitySource = NPC.GetSource_FromAI();
                    var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ModContent.ProjectileType<Psyfireball>(), NPC.damage / 2, 0, Main.myPlayer);

                    projectile.timeLeft = 300;

                    attackCounter = 500;
                    NPC.netUpdate = true;
                }
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
	}

	internal class PsywormBody : Psyworm
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
            NPC.width = 50;
            NPC.height = 76;
            NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 1000;
			NPC.defense = 10;
			NPC.damage = 20;
		}

        public override void CustomBehavior()
        {
			
        }
	}

	internal class PsywormTail : Psyworm
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
            NPC.width = 50;
            NPC.height = 76;
            NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 1000;
			NPC.defense = 10;
			NPC.damage = 20;
		}

		public override void CustomBehavior()
		{
			
        }

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}

	public abstract class Psyworm : Worm
    {
		public override void Init()
		{
			minLength = 12;
			maxLength = 24;
			tailType = ModContent.NPCType<PsywormTail>();
			bodyType = ModContent.NPCType<PsywormBody>();
			headType = ModContent.NPCType<PsywormHead>();
			speed = 20f;
			turnSpeed = 0.15f;
			flies = true;
		}
	}
}
