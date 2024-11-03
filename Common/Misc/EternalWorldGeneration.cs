using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Eternal.Common.Misc
{
    public class EternalWorldGeneration : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int GraniteIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
            int DungeonIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Dungeon"));
            if (GraniteIndex > -1)
                tasks.Insert(GraniteIndex + 1, new PassLegacy("Beneath", EternalWorldGenerationPass.GenBeneath));

            if (DungeonIndex > -1)
                tasks.Insert(DungeonIndex + 1, new PassLegacy("Shrine", EternalWorldGenerationPass.GenShrine));
        }
    }
}
