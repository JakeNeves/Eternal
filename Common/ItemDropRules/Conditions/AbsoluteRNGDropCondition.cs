using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class AbsoluteRNGDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return ModContent.GetInstance<ServerConfig>().absoluteRNGDrops;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "This is an Absolute RNG drop rate";
        }
    }
}
