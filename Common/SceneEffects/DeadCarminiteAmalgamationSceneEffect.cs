using Terraria;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;

namespace Eternal.Common.SceneEffects
{
    public class DeadCarminiteAmalgamationSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/SpinningDread");

        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<DeadCarminiteAmalgamation>());

        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
    }
}