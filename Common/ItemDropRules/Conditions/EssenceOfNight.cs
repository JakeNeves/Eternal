using Eternal.Common.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class EssenceOfNight : IItemDropRuleCondition
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
                && info.player.ZoneCorrupt
                || info.player.ZoneCrimson;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops in Underground Crimson or Corrpution";
        }
    }
}
