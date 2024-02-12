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
		public static bool downedDroxOverlord = false;
		public static bool downedEmpraynia = false;

		// post-moon lord bosses
		public static bool downedCosmicApparition = false;
        public static bool downedFrostKing = false;
        public static bool downedArkofImperious = false;
		public static bool downedCosmicEmperor = false;
		public static bool downedTrinity = false;

        // rift
        public static bool downedRiftCosmicApparition = false;
        public static bool downedRiftArkofImperious = false;

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
			downedDroxOverlord = false;
			downedEmpraynia = false;

			// post-moon lord bosses
			downedCosmicApparition = false;
            downedFrostKing = false;
            downedArkofImperious = false;
			downedCosmicEmperor = false;
			downedTrinity = false;

            // rift
            downedRiftCosmicApparition = false;
			downedRiftArkofImperious = false;
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
			downedDroxOverlord = false;
			downedEmpraynia = false;

			// post-moon lord bosses
			downedCosmicApparition = false;
            downedFrostKing = false;
            downedArkofImperious = false;
			downedTrinity = false;
			downedCosmicEmperor = false;

            // rift
            downedRiftCosmicApparition = false;
            downedRiftArkofImperious = false;
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

            // post-moon lord bosses
            if (downedCosmicApparition)
			{
				tag["downedCosmicApparition"] = true;
			}
			if (downedArkofImperious)
			{
				tag["downedArkofImperious"] = true;
			}
            if (downedCosmicEmperor)
            {
                tag["downedCosmicEmperor"] = true;
            }

            // rift
            if (downedRiftCosmicApparition)
            {
                tag["downedRiftCosmicApparition"] = true;
            }
            if (downedRiftArkofImperious)
            {
                tag["downedRiftArkofImperious"] = true;
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

            // post-moon lord bosses
            downedCosmicApparition = tag.ContainsKey("downedCosmicApparition");
			downedArkofImperious = tag.ContainsKey("downedArkofImperious");
			downedCosmicEmperor = tag.ContainsKey("downedCosmicEmperor");

            // rift
            downedRiftCosmicApparition = tag.ContainsKey("downedRiftCosmicApparition");
            downedRiftArkofImperious = tag.ContainsKey("downedRiftArkofImperious");
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

            // post-moon lord bosses
            flags[9] = downedCosmicApparition;
			flags[19] = downedArkofImperious;
            flags[22] = downedCosmicEmperor;

            // rift
            downedRiftCosmicApparition = flags[10];
            downedRiftArkofImperious = flags[20];

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

            // post-moon lord bosses
            downedCosmicApparition = flags[9];
			downedArkofImperious = flags[19];
            downedCosmicEmperor = flags[22];

            // rift
            downedRiftCosmicApparition = flags[9];
            downedRiftArkofImperious = flags[19];
        }
	}
}
