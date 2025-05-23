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

        public static bool downedDarkMoon = false;
        public static bool downedDarkMoon2 = false;

        public override void PreUpdateTime()
        {
            if (Main.dayTime)
                darkMoon = false;
        }

        public override void OnWorldLoad()
		{
            isRiftOpen = false;
			darkMoon = false;

            downedDarkMoon = false;
            downedDarkMoon2 = false;
		}

		public override void OnWorldUnload()
		{
            isRiftOpen = false;
			darkMoon = false;

            downedDarkMoon = false;
            downedDarkMoon2 = false;
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

            if (downedDarkMoon)
            {
                tag["downedDarkMoon"] = true;
            }
            if (downedDarkMoon2)
            {
                tag["downedDarkMoon2"] = true;
            }
        }

		public override void LoadWorldData(TagCompound tag)
		{
            isRiftOpen = tag.ContainsKey("isRiftOpen");
            darkMoon = tag.ContainsKey("darkMoon");

            downedDarkMoon = tag.ContainsKey("downedDarkMoon");
            downedDarkMoon2 = tag.ContainsKey("downedDarkMoon2");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = isRiftOpen;
            flags[1] = darkMoon;

            flags[2] = downedDarkMoon;
            flags[3] = downedDarkMoon2;

            writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
            isRiftOpen = flags[0];
            darkMoon = flags[1];

            downedDarkMoon = flags[2];
            downedDarkMoon2 = flags[3];
        }
    }
}