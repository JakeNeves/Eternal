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
            int GehannaIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Gehenna"));

            if (GraniteIndex > -1)
                tasks.Insert(GraniteIndex + 1, new PassLegacy("Beneath", EternalWorldGenerationPass.GenBeneath));

            if (DungeonIndex > -1)
                tasks.Insert(DungeonIndex + 1, new PassLegacy("Shrine", EternalWorldGenerationPass.GenShrine));

            if (DungeonIndex > -1)
                tasks.Insert(DungeonIndex + 1, new PassLegacy("Gehenna", EternalWorldGenerationPass.GenGehenna));

            if (GehannaIndex > -1)
                tasks.Insert(DungeonIndex + 1, new PassLegacy("Mausoleum", EternalWorldGenerationPass.GenMausoleum));
        }
    }
}
