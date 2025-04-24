using Eternal.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class GehennaDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return ModContent.GetInstance<ZoneSystem>().zoneGehenna;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops when defeated in the Gehenna";
        }
    }
}
