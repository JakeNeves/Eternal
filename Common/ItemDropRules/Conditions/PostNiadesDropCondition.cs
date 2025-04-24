using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class PostNiadesDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return DownedBossSystem.downedNiades;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after defeating Niades";
        }
    }
}
