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
		public static bool downedNiades = false;
		public static bool downedGlare = false;

		// post-moon lord bosses
		public static bool downedCosmicApparition = false;
        public static bool downedFrostKing = false;
        public static bool downedArkofImperious = false;
        public static bool downedTrinity = false;
        public static bool downedCosmicEmperor = false;

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
			downedNiades = false;
			downedGlare = false;

			// post-moon lord bosses
			downedCosmicApparition = false;
            downedFrostKing = false;
            downedArkofImperious = false;
            downedTrinity = false;
            downedCosmicEmperor = false;
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
			downedNiades = false;
			downedGlare = false;

			// post-moon lord bosses
			downedCosmicApparition = false;
            downedFrostKing = false;
            downedArkofImperious = false;
			downedTrinity = false;
			downedCosmicEmperor = false;
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
            if (downedIncinerius)
            {
                tag["downedIncinerius"] = true;
            }
            if (downedSubzeroElemental)
            {
                tag["downedSubzeroElemental"] = true;
            }
            if (downedDuneworm)
            {
                tag["downedDuneworm"] = true;
            }
            if (downedNiades)
            {
                tag["downedNiades"] = true;
            }
            if (downedGlare)
            {
                tag["downedGlare"] = true;
            }

            // post-moon lord bosses
            if (downedCosmicApparition)
			{
				tag["downedCosmicApparition"] = true;
			}
			if (downedArkofImperious)
			{
				tag["downedArkofImperious"] = true;
			}
            if (downedTrinity)
            {
                tag["downedTrinity"] = true;
            }
            if (downedCosmicEmperor)
            {
                tag["downedCosmicEmperor"] = true;
            }
        }

		public override void LoadWorldData(TagCompound tag)
		{
			// pre-hardmode bosses
			downedCarminiteAmalgamation = tag.ContainsKey("downedCarminiteAmalgamation");
			downedDuneGolem = tag.ContainsKey("downedDuneGolem");

			// hardmode bosses
			downedIgneopede = tag.ContainsKey("downedIgneopede");
            downedIncinerius = tag.ContainsKey("downedIncinerius");
            downedSubzeroElemental = tag.ContainsKey("downedSubzeroElemental");
            downedDuneworm = tag.ContainsKey("downedDuneworm");
            downedNiades = tag.ContainsKey("downedNiades");
            downedGlare = tag.ContainsKey("downedGlare");

            // post-moon lord bosses
            downedCosmicApparition = tag.ContainsKey("downedCosmicApparition");
			downedArkofImperious = tag.ContainsKey("downedArkofImperious");
            downedTrinity = tag.ContainsKey("downedTrinity");
            downedCosmicEmperor = tag.ContainsKey("downedCosmicEmperor");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			// pre-hardmode bosses
			flags[0] = downedCarminiteAmalgamation;
			flags[1] = downedDuneGolem;

			// hardmode bosses
			flags[2] = downedIgneopede;
            flags[3] = downedIncinerius;
            flags[4] = downedSubzeroElemental;
            flags[5] = downedDuneworm;
            flags[6] = downedNiades;
            flags[7] = downedGlare;

            // post-moon lord bosses
            flags[9] = downedCosmicApparition;
			flags[19] = downedArkofImperious;
            flags[20] = downedTrinity;
            flags[22] = downedCosmicEmperor;

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
            downedIncinerius = flags[3];
            downedSubzeroElemental = flags[4];
            downedDuneworm = flags[5];
            downedNiades = flags[6];
            downedGlare = flags[7];

            // post-moon lord bosses
            downedCosmicApparition = flags[9];
			downedArkofImperious = flags[19];
            downedTrinity = flags[20];
            downedCosmicEmperor = flags[22];
        }
	}
}
