using Terraria;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.Trinity;

namespace Eternal.Common.SceneEffects
{
    public class TrinitySceneEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<TrinityCore>());

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
}