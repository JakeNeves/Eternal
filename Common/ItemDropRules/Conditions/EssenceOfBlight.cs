using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class EssenceOfBlight : IItemDropRuleCondition
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
                && info.player.InModBiome<UndergroundCarrion>();
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops in Underground Carrion";
        }
    }
}
