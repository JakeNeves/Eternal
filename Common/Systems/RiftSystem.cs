using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class RiftSystem : ModSystem
    {
        public static bool isRiftOpen = false;

		public override void OnWorldLoad()
		{
            isRiftOpen = false;
		}

		public override void OnWorldUnload()
		{
            isRiftOpen = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (isRiftOpen)
			{
				tag["isRiftOpen"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
            isRiftOpen = tag.ContainsKey("isRiftOpen");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = isRiftOpen;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
            isRiftOpen = flags[0];
		}
	}
}