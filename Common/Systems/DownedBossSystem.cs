using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
		// pre-hardmode bosses
		public static bool downedCarminiteAmalgamation = false;
		public static bool downedDuneGolem = false;

		// hardmode bosses
		public static bool downedIgneopede = false;
		public static bool downedIncinerius = false;
		public static bool downedSubzeroElemental = false;
		public static bool downedDuneworm = false;
		public static bool downedEmpraynia = false;
		public static bool downedExoVlitchRipper = false;
		public static bool downedDroxOverlord = false;

		// post-moon lord bosses
		public static bool downedFrostKing = false;
		public static bool downedCosmicApparition = false;
		public static bool downedExoVlitchMechapede = false;
		public static bool downedExoVlitchThanatophane = false;
		public static bool downedExoVlitchArkasama = false;
        #region bionic bosses
        public static bool downedBionicBossAny = false;
		public static bool downedAtlas = false;
		public static bool downedBorealis = false;
		public static bool downedCeres = false;
		public static bool downedOrion = false;
		public static bool downedPolarus = false;
		public static bool downedTriplets = false;
        #endregion
        public static bool downedArkofImperious = false;
		public static bool downedNeoxBosses = false;
		public static bool downedCosmicEmperor = false;
		public static bool downedTrinity = false;

		public override void OnWorldLoad()
		{
			// pre-hardmode bosses
			downedCarminiteAmalgamation = false;
			downedDuneGolem = false;

			// hardmode bosses
			downedIgneopede = false;
			downedIncinerius = false;
			downedSubzeroElemental = false;
			downedDuneworm = false;
			downedEmpraynia = false;
			downedExoVlitchRipper = false;
			downedDroxOverlord = false;

			// post-moon lord bosses
			downedFrostKing = false;
			downedCosmicApparition = false;
			downedExoVlitchMechapede = false;
			downedExoVlitchThanatophane = false;
			downedExoVlitchArkasama = false;
			downedAtlas	= false;
			downedBorealis = false;
			downedCeres = false;
			downedOrion = false;
			downedPolarus = false;
			downedTriplets = false;
			downedArkofImperious = false;
			downedNeoxBosses = false;
			downedCosmicEmperor = false;
			downedTrinity = false;
		}

		public override void OnWorldUnload()
		{
			// pre-hardmode bosses
			downedCarminiteAmalgamation = false;
			downedDuneGolem = false;

			// hardmode bosses
			downedIgneopede = false;
			downedIncinerius = false;
			downedSubzeroElemental = false;
			downedDuneworm = false;
			downedEmpraynia = false;
			downedExoVlitchRipper = false;
			downedDroxOverlord = false;

			// post-moon lord bosses
			downedFrostKing = false;
			downedCosmicApparition = false;
			downedExoVlitchMechapede = false;
			downedExoVlitchThanatophane = false;
			downedExoVlitchArkasama = false;
			downedAtlas = false;
			downedBorealis = false;
			downedCeres = false;
			downedOrion = false;
			downedPolarus = false;
			downedTriplets = false;
			downedArkofImperious = false;
			downedNeoxBosses = false;
			downedCosmicEmperor = false;
			downedTrinity = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{
			// pre-hardmode bosses
			if (downedCarminiteAmalgamation)
			{
				tag["downedCarminiteAmalgamation"] = true;
			}
			if (downedDuneGolem)
			{
				tag["downedDuneGolem"] = true;
			}

			// hardmode bosses
			if (downedIgneopede)
			{
				tag["downedIgneopede"] = true;
			}

			// post-moon lord bosses
			if (downedCosmicApparition)
			{
				tag["downedCosmicApparition"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			// pre-hardmode bosses
			downedCarminiteAmalgamation = tag.ContainsKey("downedCarminiteAmalgamation");
			downedDuneGolem = tag.ContainsKey("downedDuneGolem");

			// hardmode bosses
			downedDuneGolem = tag.ContainsKey("downedIgneopede");

			// post-moon lord bosses
			downedCosmicApparition = tag.ContainsKey("downedCosmicApparition");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			// pre-hardmode bosses
			flags[0] = downedCarminiteAmalgamation;
			flags[1] = downedDuneGolem;

			// hardmode bosses
			flags[2] = downedIgneopede;

			// post-moon lord bosses
			flags[10] = downedCosmicApparition;

			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			// pre-hardmode bosses
			downedCarminiteAmalgamation = flags[0];
			downedDuneGolem = flags[1];

			// hardmode bosses
			downedIgneopede = flags[2];

			// post-moon lord bosses
			downedCosmicApparition = flags[10];
		}
	}
}
