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
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Carrion
{
	internal class GiantLeechHead : GiantLeech
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override void SetStaticDefaults()
		{
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
				CustomTexturePath = "Eternal/Content/NPCs/Carrion/GiantLeech_Preview",
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
            NPC.HitSound = SoundID.NPCDeath22;
            NPC.DeathSound = SoundID.NPCDeath12;
			NPC.lifeMax = 120;
			NPC.defense = 16;
			NPC.damage = 25;
			NPC.value = Item.sellPrice(gold: 6, silver: 10);
			SpawnModBiomes = [ ModContent.GetInstance<Biomes.CarrionSurface>().Type ];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (ModContent.GetInstance<ZoneSystem>().zoneCarrion)
                return SpawnCondition.Overworld.Chance * 1.5f;
			else
				return SpawnCondition.Overworld.Chance * 0f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange([
				new FlavorTextBestiaryInfoElement("These leeches somehow evolved and adapted to the dead climate of the Carrion...")
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

	internal class GiantLeechBody : GiantLeech
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

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
            NPC.HitSound = SoundID.NPCDeath22;
            NPC.DeathSound = SoundID.NPCDeath12;
			NPC.lifeMax = 520;
			NPC.defense = 10;
			NPC.damage = 20;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
        }

        public override void CustomBehavior()
        {
        }
	}

	internal class GiantLeechTail : GiantLeech
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

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
            NPC.HitSound = SoundID.NPCDeath22;
            NPC.DeathSound = SoundID.NPCDeath12;
           	NPC.lifeMax = 520;
			NPC.defense = 10;
			NPC.damage = 20;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            for (int k = 0; k < 5.0; k++)
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, 0, 0, 0, default(Color), 1f);
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

	public abstract class GiantLeech : Worm
    {
		public override void Init()
		{
			minLength = 2;
			maxLength = 4;
			tailType = ModContent.NPCType<GiantLeechTail>();
			bodyType = ModContent.NPCType<GiantLeechBody>();
			headType = ModContent.NPCType<GiantLeechHead>();
			speed = 15f;
			turnSpeed = 0.15f;
			flies = false;
		}
	}
}
