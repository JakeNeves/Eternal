using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
		public static bool downedCarminiteAmalgamation = false;
		public static bool downedDuneGolem = false;

		public override void OnWorldLoad()
		{
			downedCarminiteAmalgamation = false;
			downedDuneGolem = false;
		}

		public override void OnWorldUnload()
		{
			downedCarminiteAmalgamation = false;
			downedDuneGolem = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{
			if (downedCarminiteAmalgamation)
			{
				tag["downedCarminiteAmalgamation"] = true;
			}
			if (downedDuneGolem)
			{
				tag["downedDuneGolem"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedCarminiteAmalgamation = tag.ContainsKey("downedCarminiteAmalgamation");
			downedDuneGolem = tag.ContainsKey("downedDuneGolem");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = downedCarminiteAmalgamation;
			flags[1] = downedDuneGolem;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedCarminiteAmalgamation = flags[0];
			downedDuneGolem = flags[1];
		}
	}
}
