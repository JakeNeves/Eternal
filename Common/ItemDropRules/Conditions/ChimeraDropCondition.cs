using Eternal.Common.Systems;
using Eternal.Content.NPCs.Boss.TheChimera;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class ChimeraDropCondition : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            int type = ModContent.NPCType<TheHeap>();

            if (info.npc.type == ModContent.NPCType<TheHeap>())
                type = ModContent.NPCType<TheHive>();

            return !NPC.AnyNPCs(type);
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
