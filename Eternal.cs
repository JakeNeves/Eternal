using Eternal.NPCs.Boss.Incinerius;
using Terraria.Graphics.Effects;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Eternal.Items.Summon;
using System.Collections.Generic;
using Eternal.Items;
using Eternal.Items.Tools;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.MusicBox;
using Eternal.Items.Armor;
using Eternal.Items.BossBags;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Summon;

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

			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath"), ItemType("DunekeeperMusicBox"), TileType("DunekeeperMusicBox"));
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/FieryBattler"), ItemType("IncineriusMusicBox"), TileType("IncineriusMusicBox"));
			#endregion

			if (Main.netMode != NetmodeID.Server)
            {
				#region sky things
				//Filters.Scene["Eternal:TrueIncinerius"] = new Filter(new TrueIncineriusScreenShaderData("FilterMiniTower").UseColor(0.175f, 0.75f, 0.255f).UseOpacity(0.75f), EffectPriority.VeryHigh);
				//SkyManager.Instance["Eternal:TrueIncinerius"] = new TrueIncineriusSky();
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

		}

        public override void PostSetupContent()
        {
			#region Boss Checklist Intigration
			Mod bossCheckList = ModLoader.GetMod("BossCheckList");
			if (bossCheckList != null)
            {
				bossCheckList.Call(
					"AddBoss",
					5.4f,
					ModContent.NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>(),
					this,
					"The Carmanite Scouter",
					(Func<bool>)(() => EternalWorld.downedCarmaniteScouter),
					0,
					ModContent.ItemType<SuspiciousLookingDroplet>(),
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

				bossCheckList.Call(
					"AddBoss",
					6.0f,
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
					"The Subzero Elemental",
					(Func<bool>)(() => EternalWorld.downedIncinerius),
					ModContent.ItemType<AncientGlacialInscription>(),
					0,
					new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>() },
					"Spawn by using [i:" + ModContent.ItemType<AncientGlacialInscription>() + "] in the snow biome.",
					"The Living Kelvin Construct",
					"Eternal/BossChecklist/SubzeroElemental",
					"Eternal/NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"
				);
			}
			#endregion
		}

    }
}