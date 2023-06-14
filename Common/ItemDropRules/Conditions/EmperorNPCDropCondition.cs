using Eternal.Content.NPCs.Town;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class EmperorNPCDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return NPC.AnyNPCs(ModContent.NPCType<Emperor>());
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops if the Emperor has settled in your world";
        }
    }
}
