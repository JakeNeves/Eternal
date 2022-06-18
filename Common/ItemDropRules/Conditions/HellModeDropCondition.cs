using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class HellModeDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return DifficultySystem.hellMode;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "This is a Hell Mode drop rate";
        }
    }
}
