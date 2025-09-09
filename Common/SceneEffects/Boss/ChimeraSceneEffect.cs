using Terraria;
using Terraria.ModLoader;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.TheChimera;

namespace Eternal.Common.SceneEffects.Boss
{
    public class ChimeraSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AmalgamatedHorror");

        public override bool IsSceneEffectActive(Player player) => NPC.AnyNPCs(ModContent.NPCType<TheChimera>());

        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
    }
}