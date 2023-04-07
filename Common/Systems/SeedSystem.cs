using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace Eternal.Common.Systems
{
    public class SeedSystem : ModSystem
    {
        public static bool emperorSeed = false;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            if (!(WorldGen.currentWorldSeed == "emperor" || WorldGen.currentWorldSeed == "03232004" || WorldGen.currentWorldSeed == "jake" || WorldGen.currentWorldSeed == "praise the jake"))
            {
                return;
            }
            else
            {
                emperorSeed = true;
            }
        }

        public override void OnWorldLoad()
        {
            emperorSeed = false;
        }

        public override void OnWorldUnload()
        {
            emperorSeed = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (emperorSeed)
            {
                tag["emperorSeed"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            emperorSeed = tag.ContainsKey("emperorSeed");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = emperorSeed;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            emperorSeed = flags[0];
        }
    }
}
