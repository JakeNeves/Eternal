using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace Eternal.Common.ItemDropRules.Conditions
{
    public class SoulOfBlight : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;

            return Main.hardMode
                && DownedBossSystem.downedChimera
                && !NPCID.Sets.CannotDropSouls[npc.type]
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
