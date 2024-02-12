using Eternal.Common.Configurations;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class LifeMoteExperimentalFeatureCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return ServerConfig.instance.lifeMotes;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "The 'Life Motes' experimental feature must be toggled in the Server Configuration settings";
        }
    }
}
