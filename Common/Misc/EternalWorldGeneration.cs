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
            if (GraniteIndex > -1)
                tasks.Insert(GraniteIndex + 1, new PassLegacy("Beneath", EternalWorldGenerationPass.GenBeneath));

            int MarbleIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Marble"));
            if (MarbleIndex > -1)
                tasks.Insert(MarbleIndex + 1, new PassLegacy("PrecursorHollows", EternalWorldGenerationPass.GenPrecursorHollows));

            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("LivingTrees"));
            if (LivingTreesIndex > -1)
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Structures", EternalWorldGenerationPass.GenerateStructures));
        }
    }
}
