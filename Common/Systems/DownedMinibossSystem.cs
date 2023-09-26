using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class DownedMinibossSystem : ModSystem
    {
        public static bool downedStarbornInquisitor = false;
        public static bool downedFakeArkofImperious = false;
        public static bool downedPhantomConstruct = false;

        public override void OnWorldLoad()
		{
            downedStarbornInquisitor = false;
            downedFakeArkofImperious = false;
            downedPhantomConstruct = false;
		}

		public override void OnWorldUnload()
		{
            downedStarbornInquisitor = false;
            downedFakeArkofImperious = false;
            downedPhantomConstruct = false;
        }
		public override void SaveWorldData(TagCompound tag)
		{
            if (downedStarbornInquisitor)
            {
                tag["downedStarbornInquisitor"] = true;
            }
            if (downedFakeArkofImperious)
            {
                tag["downedFakeArkofImperious"] = true;
            }
            if (downedPhantomConstruct)
            {
                tag["downedPhantomConstruct"] = true;
            }
        }

		public override void LoadWorldData(TagCompound tag)
		{
            downedStarbornInquisitor = tag.ContainsKey("downedStarbornInquisitor");
            downedFakeArkofImperious = tag.ContainsKey("downedFakeArkofImperious");
            downedPhantomConstruct = tag.ContainsKey("downedPhantomConstruct");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
            flags[0] = downedStarbornInquisitor;
            flags[1] = downedFakeArkofImperious;
            flags[2] = downedPhantomConstruct;

            writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();

            downedStarbornInquisitor = flags[0];
            downedFakeArkofImperious = flags[1];
            downedPhantomConstruct = flags[2];
        }
	}
}
