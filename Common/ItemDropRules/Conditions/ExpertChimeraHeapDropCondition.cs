using Eternal.Common.Systems;
using Eternal.Content.NPCs.Boss.TheChimera;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class ExpertChimeraHeapDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return Main.expertMode && !NPC.AnyNPCs(ModContent.NPCType<TheHive>());
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "";
        }
    }
}
