using Eternal.Common.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class EssenceOfLight : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;

            return Main.hardMode
                && DownedBossSystem.downedGlare
                && !EternalCommonUtils.CannotDropEssences[npc.type]
                && !npc.boss
                && npc.lifeMax > 1
                && npc.value >= 1f
                && info.player.ZoneHallow;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops in Underground Hallow";
        }
    }
}
