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
        public static bool everythingSeed = false;

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            if (!(WorldGen.currentWorldSeed == "emperor" || WorldGen.currentWorldSeed == "03232004" || WorldGen.currentWorldSeed == "jake" || WorldGen.currentWorldSeed == "praise the jake"))
                return;
            else
                emperorSeed = true;

            if (!(WorldGen.currentWorldSeed == "everything everywhere all at once" || WorldGen.currentWorldSeed == "everyone is here"))
                return;
            else
            {
                everythingSeed = true;
                Main.getGoodWorld = true;
                Main.zenithWorld = true;
                Main.drunkWorld = true;
                Main.remixWorld = true;
            }
        }

        public override void OnWorldLoad()
        {
            emperorSeed = false;
            everythingSeed = false;
        }

        public override void OnWorldUnload()
        {
            emperorSeed = false;
            everythingSeed = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (emperorSeed)
            {
                tag["emperorSeed"] = true;
            }
            if (everythingSeed)
            {
                tag["everythingSeed"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            emperorSeed = tag.ContainsKey("emperorSeed");
            everythingSeed = tag.ContainsKey("everythingSeed");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = emperorSeed;
            flags[1] = everythingSeed;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            emperorSeed = flags[0];
            everythingSeed = flags[1];
        }
    }
}
