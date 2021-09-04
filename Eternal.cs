using Terraria.Graphics.Effects;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Eternal.Items.Summon;
using System.Collections.Generic;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.MusicBox;
using Eternal.Items.Armor;
using Eternal.Items.BossBags;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Summon;
using Eternal.NPCs.Boss.Empraynia;
using Eternal.Items.Materials;
using Eternal.Items.Placeable;
using Eternal.Items.Weapons.Throwing;
using Eternal.Skies;
using Terraria.Graphics.Shaders;
using Terraria.UI;
using Eternal.Items.Potions;
using Eternal.Items.Accessories.Expert;
using Eternal.Items.Weapons.Expert;
using Microsoft.Xna.Framework;
//using Eternal.Integration;

namespace Eternal
{
	public class Eternal : Mod
	{
        internal static Mod instance;

        public Eternal()
		{

			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
				AutoloadBackgrounds = true
			};

		}

        public override void Unload()
        {
			instance = null;
        }

        public override void Load()
        {
			#region Music Boxes
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MazesAndLivingSwords"), ItemType("LabrynthMusicBox"), TileType("LabrynthMusicBox"));
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DeepDark"), ItemType("BeneathMusicBox"), TileType("BeneathMusicBox"));

			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/BladeofBrutality"), ItemType("AoIMusicBox"), TileType("AoIMusicBox"));
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath"), ItemType("DunekeeperMusicBox"), TileType("DunekeeperMusicBox"));
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/FieryBattler"), ItemType("IncineriusMusicBox"), TileType("IncineriusMusicBox"));
            #endregion

            if (Main.netMode != NetmodeID.Server)
            {
				#region sky things
				Filters.Scene["Eternal:Empraynia"] = new Filter(new EmprayniaScreenShaderData("FilterMiniTower").UseColor(0.229f, 0.84f, 0.255f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["Eternal:Empraynia"] = new EmprayniaSky();

				Filters.Scene["Eternal:AshpitSky"] = new Filter((new ScreenShaderData("FilterMiniTower")).UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryLow);
				SkyManager.Instance["Eternal:AshpitSky"] = new AshpitSky();
				#endregion
			}
		}

		public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
			if(Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
				return;
            }

			if(Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneThunderduneBiome)
            {
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/VitreousSandsofThunder");
				priority = MusicPriority.BiomeMedium;
            }

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet)
			{
				if (!ModContent.GetInstance<EternalConfig>().originalMusic)
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/New/AstralDiscovery");
					priority = MusicPriority.BiomeMedium;
				}
				else
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/ShatteredStar");
					priority = MusicPriority.BiomeMedium;
				}
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth)
			{
				if (!ModContent.GetInstance<EternalConfig>().originalMusic)
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/New/ImperiousShrine");
					priority = MusicPriority.BiomeMedium;
				}
				else
				{
					music = GetSoundSlot(SoundType.Music, "Sounds/Music/MazesAndLivingSwords");
					priority = MusicPriority.BiomeMedium;
				}
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath)
			{
				//music = MusicID.Eerie;
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/DeepDark");
				priority = MusicPriority.BiomeMedium;
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneAshpit)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/AshFields");
				priority = MusicPriority.BiomeMedium;
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().droxEvent)
			{
				 music = GetSoundSlot(SoundType.Music, "Sounds/Music/MechanicalEnvy");
				 priority = MusicPriority.BossMedium;
			}

		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
        }

		#region Eternal Mod Integration
		//Calamity Mod Integration
		//public CalamityIntegration CalamityIntegration { get; private set; }
		//public bool CalamityLoaded => CalamityIntegration != null;

		//Fargo's Mod Integration
		//public FargoModIntegration FargoModIntegration { get; private set; }
		//public bool FargowiltasModLoaded => FargoModIntegration != null;
		#endregion

		public static bool NoInvasion(NPCSpawnInfo spawnInfo)
		{
			return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
				(!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

		public override void PostSetupContent()
        {
            /*try
            {
				CalamityIntegration = new CalamityIntegration(this).TryLoad() as CalamityIntegration;
				FargoModIntegration = new FargoModIntegration(this).TryLoad() as FargoModIntegration;
            }
			catch (Exception e)
            {
				Logger.Warn("Eternal PostSetupContent Error: " + e.StackTrace + e.Message);
            }*/

			#region Boss Checklist Integration
			Mod bossCheckList = ModLoader.GetMod("BossCheckList");
			if (bossCheckList != null)
            {
				//Pre-Hardmode
				bossCheckList.Call(
					"AddBoss",
					5.4f,
					ModContent.NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>(),
					this,
					"The Carmanite Scouter",
					(Func<bool>)(() => EternalWorld.downedCarmaniteScouter),
					ModContent.ItemType<SuspiciousLookingDroplet>(),
					0,
					new List<int> { ModContent.ItemType<CarmaniteScouterBag>(), ModContent.ItemType<Bloodtooth>(), ModContent.ItemType<Carmanite>(), ModContent.ItemType<CarmaniteBane>(), ModContent.ItemType<CarmaniteRipperClaws>(), ModContent.ItemType<BruteCleavage>(), ModContent.ItemType<CarmanitePurgatory>(), ModContent.ItemType<CarmaniteDeadshot>(), ItemID.LesserHealingPotion },
					"Spawn by using the [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() +"]",
					"The Blood-Feasting Amalgamate.",
					"Eternal/BossChecklist/CarmaniteScouter",
					"Eternal/NPCs/Boss/CarmaniteScouter/CarmaniteScouter_Head_Boss"
				);

				bossCheckList.Call(
					"AddBoss",
					5.6f,
					ModContent.NPCType<NPCs.Boss.Dunekeeper.Dunekeeper>(),
					this,
					"Dunekeeper",
					(Func<bool>)(() => EternalWorld.downedDunekeeper),
					ModContent.ItemType<RuneofThunder>(),
					new List<int> { ModContent.ItemType<DunekeeperMusicBox>() },
					new List<int> { ModContent.ItemType<DunekeeperBag>(), ModContent.ItemType<PrimordialBolt>(), ModContent.ItemType<DuneCore>(), ModContent.ItemType<StormBeholder>(), ModContent.ItemType<ThunderduneHeadgear>(), ModContent.ItemType<Wasteland>(), ItemID.LesserHealingPotion },
					"Spawn by using the [i:" + ModContent.ItemType<RuneofThunder>() + "] in the desert.",
					"The Unstabe Thundergen of the Desert.",
					"Eternal/BossChecklist/Dunekeeper",
					"Eternal/NPCs/Boss/Dunekeeper/Dunekeeper_Head_Boss"
				);

				//Hardmode
				bossCheckList.Call(
					"AddBoss",
					7f,
					ModContent.NPCType<NPCs.Boss.Incinerius.Incinerius>(),
					this,
					"Incinerius",
					(Func<bool>)(() => EternalWorld.downedIncinerius),
					ModContent.ItemType<RelicofInferno>(),
					new List<int> { ModContent.ItemType<IncineriusMusicBox>() },
					new List<int> { ModContent.ItemType<IncineriusBag>(), ModContent.ItemType<FlameInfusedJewel>(), ModContent.ItemType<ScorchedMetal>(), ModContent.ItemType<SmotheringInferno>(), ModContent.ItemType<FuryFlare>(), ModContent.ItemType<Pyroyo>(), ItemID.GreaterHealingPotion },
					"Spawn by using the [i:" + ModContent.ItemType<RelicofInferno>() + "] in the underworld.",
					"The Flaming Golem of the Underworld",
					"Eternal/BossChecklist/Incinerius",
					"Eternal/NPCs/Boss/Incinerius/Incinerius_Head_Boss"
				);

				bossCheckList.Call(
					"AddBoss",
					10.6f,
					ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>(),
					this,
					"Subzero Elemental",
					(Func<bool>)(() => EternalWorld.downedSubzeroElemental),
					ModContent.ItemType<AncientGlacialInscription>(),
					0,
					new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<FrostKingsCore>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>(), ItemID.GreaterHealingPotion },
					"Spawn by using the [i:" + ModContent.ItemType<AncientGlacialInscription>() + "] in the snow biome.",
					"The Living Kelvin Construct",
					"Eternal/BossChecklist/SubzeroElemental",
					"Eternal/NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"
				);

				//Post-Moon Lord
				bossCheckList.Call(
					"AddBoss",
					15f,
					ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>(),
					this,
					"Subzero Elemental (Post-Moon Lord)",
					(Func<bool>)(() => EternalWorld.downedSubzeroElementalP2),
					ModContent.ItemType<AncientGlacialInscription>(),
					0,
					new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<FrostKingsCore>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>(), ModContent.ItemType<SydaniteOre>(), ItemID.SuperHealingPotion},
					"Refight the Subzero Elemental Post-Moon Lord.",
					"The Living Kelvin Construct is Back",
					"Eternal/BossChecklist/SubzeroElemental",
					"Eternal/NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"
				);

				bossCheckList.Call(
					"AddBoss",
					15.5f,
					ModContent.NPCType<NPCs.Boss.CosmicApparition.CosmicApparition>(),
					this,
					"Cosmic Apparition",
					(Func<bool>)(() => EternalWorld.downedCosmicApparition),
					ModContent.ItemType<HyperloadedAffliction>(),
					0,
					new List<int> { ModContent.ItemType<CosmicApparitionBag>(), ModContent.ItemType<ApparitionalRendingStaff>(), ModContent.ItemType<ApparitionalDisk>(), ModContent.ItemType<CosmicFist>(), ModContent.ItemType<Cometstorm>(), ModContent.ItemType<PristineHealingPotion>() },
					"Spawn by Killing a Soul Crystal after defeating Empraynia, Post-Moon Lord.",
					"The Ghostly Smile of Someone Powerful",
					"Eternal/BossChecklist/CosmicApparition",
					"Eternal/NPCs/Boss/CosmicApparition/CosmicAppatition_Head_Boss"
				);

				bossCheckList.Call(
					"AddBoss",
					15.75f,
					ModContent.NPCType<NPCs.Boss.AoI.ArkofImperious>(),
					this,
					"Ark of Imperious",
					(Func<bool>)(() => EternalWorld.downedArkOfImperious),
					ModContent.ItemType<RoyalLabrynthSword>(),
					new List<int> { ModContent.ItemType<AoIMusicBox>() },
					new List<int> { ModContent.ItemType<AoIBag>(), ModContent.ItemType<TheImperiousCohort>(), ModContent.ItemType<TheEnigma>(), ModContent.ItemType<DormantHeroSword>(), ModContent.ItemType<Arkbow>(), ModContent.ItemType<PristineHealingPotion>() },
					"Spawn by using the [i:" + ModContent.ItemType<RoyalLabrynthSword>() + "] at the shrine",
					"The mighty imperial blade of the shrine",
					"Eternal/BossChecklist/ArkofImperious",
					"Eternal/NPCs/Boss/AoI/ArkofImperious_Head_Boss"
				);

			}
			#endregion
			#region YABHBM Integration
			Mod FKBossHealthBar = ModLoader.GetMod("FKBossHealthBar");
			if (FKBossHealthBar != null)
            {
				// Incinerius
				FKBossHealthBar.Call("hbStart");
				FKBossHealthBar.Call("hbSetTexture",
					GetTexture("BossBars/IncineriusBarStart"),
					GetTexture("BossBars/IncineriusBarMiddle"),
					GetTexture("BossBars/IncineriusBarEnd"),
					GetTexture("BossBars/IncineriusBarFill"));
				FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/Incinerius/Incinerius_Head_Boss"));
				FKBossHealthBar.Call("hbSetColours",
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f));
				FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.Incinerius.Incinerius>());
				// Subzero Elemental
				FKBossHealthBar.Call("hbStart");
				FKBossHealthBar.Call("hbSetTexture",
					GetTexture("BossBars/SubzeroElementalBarStart"),
					GetTexture("BossBars/SubzeroElementalBarMiddle"),
					GetTexture("BossBars/SubzeroElementalBarEnd"),
					GetTexture("BossBars/SubzeroElementalBarFill"));
				FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"));
				FKBossHealthBar.Call("hbSetColours",
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f));
				FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>());
				// AoI
				FKBossHealthBar.Call("hbStart");
				FKBossHealthBar.Call("hbSetTexture",
					GetTexture("BossBars/AoIBarStart"),
					GetTexture("BossBars/AoIBarMiddle"),
					GetTexture("BossBars/AoIBarEnd"),
					GetTexture("BossBars/AoIBarFill"));
				FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/AoI/ArkofImperious_Head_Boss"));
				FKBossHealthBar.Call("hbSetColours",
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f),
					new Color(1f, 1f, 1f));
				FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.AoI.ArkofImperious>());
			}
			#endregion
		}

    }
}