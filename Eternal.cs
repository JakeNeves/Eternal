using Terraria.Graphics.Effects;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Eternal.Items.Summon;
using System.Collections.Generic;
using Eternal.Items;
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
				priority = MusicPriority.BiomeLow;
            }

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/ShatteredStar");
				priority = MusicPriority.BiomeLow;
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/MazesAndLivingSwords");
				priority = MusicPriority.BiomeLow;
			}

			if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath)
			{
				//music = MusicID.Eerie;
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/DeepDark");
				priority = MusicPriority.BiomeLow;
			}

		}

        public override void PostSetupContent()
        {
			#region Boss Checklist Intigration
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
					new List<int> { ModContent.ItemType<CarmaniteScouterBag>(), ModContent.ItemType<Carmanite>(), ModContent.ItemType<CarmaniteBane>(), ModContent.ItemType<CarmaniteRipperClaws>(), ModContent.ItemType<BruteCleavage>(), ModContent.ItemType<CarmanitePurgatory>(), ModContent.ItemType<CarmaniteDeadshot>() },
					"Spawn by using [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() +"] during the Night.",
					"The Blood-Feasting Amalgamate of the night.",
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
					new List<int> { ModContent.ItemType<DunekeeperBag>(), ModContent.ItemType<DuneCore>(), ModContent.ItemType<StormBeholder>(), ModContent.ItemType<ThunderduneHeadgear>(), ModContent.ItemType<Wasteland>() },
					"Spawn by using [i:" + ModContent.ItemType<RuneofThunder>() + "] in the desert.",
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
					0,
					new List<int> { ModContent.ItemType<IncineriusBag>(), ModContent.ItemType<ScorchedMetal>(), ModContent.ItemType<SmotheringInferno>(), ModContent.ItemType<FuryFlare>(), ModContent.ItemType<Pyroyo>() },
					"Spawn by using [i:" + ModContent.ItemType<RelicofInferno>() + "] in the underworld.",
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
					new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>() },
					"Spawn by using [i:" + ModContent.ItemType<AncientGlacialInscription>() + "] in the snow biome.",
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
					new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>(), ModContent.ItemType<SydaniteOre>() },
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
					ModContent.ItemType<AncientGlacialInscription>(),
					0,
					new List<int> { ModContent.ItemType<CosmicApparitionBag>(), ModContent.ItemType<ApparitionalRendingStaff>(), ModContent.ItemType<ApparitionalDisk>(), ModContent.ItemType<CosmicFist>(), ModContent.ItemType<Cometstorm>() },
					"Spawn by Killing a Soul Crystal after defeating Empraynia, Post-Moon Lord.",
					"The Ghostly Smile of Someone Powerful",
					"Eternal/BossChecklist/CosmicApparition",
					"Eternal/NPCs/Boss/CosmicApparition/CosmicAppatition_Head_Boss"
				);

			}
			#endregion
		}

    }
}