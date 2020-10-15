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

        public override void Load()
        {
			#region Music Boxes
			AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath"), ItemType("DunekeeperMusicBox"), TileType("DunekeeperMusicBox"));
            #endregion

            if (Main.netMode != NetmodeID.Server)
            {
				#region sky things
				Filters.Scene["Eternal:TrueIncinerius"] = new Filter(new TrueIncineriusScreenShaderData("FilterMiniTower").UseColor(175f, 75f, 255f).UseOpacity(0.75f), EffectPriority.VeryHigh);
				SkyManager.Instance["Eternal:TrueIncinerius"] = new TrueIncineriusSky();
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
					5.5f,
					ModContent.NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>(),
					this,
					"The Carmanite Scouter",
					(Func<bool>)(() => EternalWorld.downedCarmaniteScouter),

					ModContent.ItemType<SuspiciousLookingDroplet>(),
					new List<int> { ModContent.ItemType<Carmanite>(), ModContent.ItemType<TritalodiumBar>(), ModContent.ItemType<CarmanitePickaxe>(), ModContent.ItemType<CarmaniteHammaxe>() },
					"Spawn by using [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() +"] during the Night.",
					"The Blood-Feasting Amalgamate of the night.",
					"Eternal/BossChecklist/CarmaniteScouter",
					"Eternal/NPCs/Boss/CarmaniteScouter/CarmaniteScouter_Head_Boss"
				);
            }
			#endregion
		}

    }
}