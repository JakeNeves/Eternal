using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class PostCosmicApparitionRiftDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return DownedBossSystem.downedRiftCosmicApparition;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after defeating the Cosmic Apparition in the Rift";
        }
    }
}
