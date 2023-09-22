using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class DifficultySystem : ModSystem
    {
        public static bool hellMode = false;
		public static bool sinstormMode = false;

		public override void OnWorldLoad()
		{
			hellMode = false;
			sinstormMode = false;
		}

		public override void OnWorldUnload()
		{
			hellMode = false;
			sinstormMode = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (hellMode)
			{
				tag["hellMode"] = true;
			}

			if (sinstormMode)
			{
				tag["sinstormMode"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			hellMode = tag.ContainsKey("hellMode");
			sinstormMode = tag.ContainsKey("sinstormMode");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = hellMode;
			flags[1] = sinstormMode;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			hellMode = flags[0];
			sinstormMode = flags[1];
		}
	}
}