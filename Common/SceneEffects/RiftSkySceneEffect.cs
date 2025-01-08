using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class RiftSkySceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/WanderingBeyondTheShiftingDistortionAcrossTheHorizons");

        public override bool IsSceneEffectActive(Player player) => EventSystem.isRiftOpen && player.ZoneSkyHeight;

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}