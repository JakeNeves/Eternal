using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class EventSystem : ModSystem
    {
        public static bool isRiftOpen = false;
        public static bool darkMoon = false;

        public override void OnWorldLoad()
		{
            isRiftOpen = false;
			darkMoon = false;
		}

		public override void OnWorldUnload()
		{
            isRiftOpen = false;
			darkMoon = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (isRiftOpen)
			{
				tag["isRiftOpen"] = true;
			}

            if (darkMoon)
            {
                tag["darkMoon"] = true;
            }
        }

		public override void LoadWorldData(TagCompound tag)
		{
            isRiftOpen = tag.ContainsKey("isRiftOpen");
            darkMoon = tag.ContainsKey("darkMoon");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = isRiftOpen;
            flags[1] = darkMoon;
            writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
            isRiftOpen = flags[0];
            darkMoon = flags[1];
        }
    }
}