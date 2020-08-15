using Terraria;
using Terraria.ModLoader;

namespace Eternal
{
	public class Eternal : Mod
	{
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

		}

	}
}