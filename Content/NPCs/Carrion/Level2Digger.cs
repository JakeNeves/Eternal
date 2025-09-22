using Eternal.Common.Configurations;
using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Carrion
{
	internal class Level2DiggerHead : Level2Digger
	{
        public override void SetStaticDefaults()
		{
			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
				CustomTexturePath = "Eternal/Content/NPCs/Carrion/Level2Digger_Preview",
				Position = new Vector2(0f, 24f),
				PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 8f
            	};
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.DiggerHead);
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath22;
			NPC.lifeMax = 260;
			NPC.defense = 16;
			NPC.damage = 25;
			NPC.value = Item.sellPrice(gold: 6, silver: 10);
			SpawnModBiomes = [ ModContent.GetInstance<Biomes.UndergroundCarrion>().Type ];
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenBlood, 0, 0, 0, default(Color), 1f);
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            int gore1 = Mod.Find<ModGore>("Level2DiggerGore1").Type;

            Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

            if (Main.rand.NextBool(2) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.rand.Next(1, 3); i++)
                {
                    NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, NPCID.Maggot);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion)
                return SpawnCondition.Cavern.Chance * 0.75f;
			else
				return SpawnCondition.Cavern.Chance * 0f;
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NecroticTissue>(), 1, 2, 3));
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange([
				new FlavorTextBestiaryInfoElement("A Posthumous Digger that has adapted to the dead climate of the Carrion...")
			]);
		}

		public override void CustomBehavior()
		{
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

	internal class Level2DiggerBody : Level2Digger
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
            NPC.DeathSound = SoundID.NPCDeath22;
            NPC.lifeMax = 520;
			NPC.defense = 10;
			NPC.damage = 20;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenBlood, 0, 0, 0, default(Color), 1f);

			if (NPC.life <= 0)
			{
				int gore1 = Mod.Find<ModGore>("Level2DiggerGore2").Type;

				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

                if (Main.rand.NextBool(2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.rand.Next(1, 3); i++)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, NPCID.Maggot);
                    }
                }
            }
        }

        public override void CustomBehavior()
        {
        }
	}

	internal class Level2DiggerTail : Level2Digger
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
            NPC.DeathSound = SoundID.NPCDeath22;
           	NPC.lifeMax = 520;
			NPC.defense = 10;
			NPC.damage = 20;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenBlood, 0, 0, 0, default(Color), 1f);

			if (NPC.life <= 0)
			{
                int gore1 = Mod.Find<ModGore>("Level2DiggerGore3").Type;

                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), gore1);

                if (Main.rand.NextBool(2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.rand.Next(1, 3); i++)
                    {
                        NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, NPCID.Maggot);
                    }
                }
            }
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

	public abstract class Level2Digger : Worm
    {
		public override void Init()
		{
			minLength = 18;
			maxLength = 36;
			tailType = ModContent.NPCType<Level2DiggerTail>();
			bodyType = ModContent.NPCType<Level2DiggerBody>();
			headType = ModContent.NPCType<Level2DiggerHead>();
			speed = 15f;
			turnSpeed = 0.15f;
			flies = false;
		}
	}
}
