using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class PostArkofImperiousDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return DownedBossSystem.downedArkofImperious;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after defeating the Ark of Imperious";
        }
    }
}
