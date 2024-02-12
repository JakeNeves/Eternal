using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class RiftNightSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/JourneyIntoaShiftingWorld");

        public override bool IsSceneEffectActive(Player player) => RiftSystem.isRiftOpen && !Main.dayTime && player.ZoneOverworldHeight && !player.ZoneSkyHeight;

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}