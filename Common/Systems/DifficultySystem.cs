using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class DifficultySystem : ModSystem
    {
        public static bool hellMode = false;

		public override void OnWorldLoad()
		{
			hellMode = false;
		}

		public override void OnWorldUnload()
		{
			hellMode = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{
			if (hellMode)
			{
				tag["hellMode"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			hellMode = tag.ContainsKey("hellMode");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = hellMode;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			hellMode = flags[0];
		}
	}
}