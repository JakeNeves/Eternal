using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class RiftUnderworldSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheTorridCenterofTheShiftedWorld");

        public override bool IsSceneEffectActive(Player player) => EventSystem.isRiftOpen && player.ZoneUnderworldHeight;

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}