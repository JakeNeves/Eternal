using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class RiftSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AcrossaDisfiguredReality");

        public override bool IsSceneEffectActive(Player player) => RiftSystem.isRiftOpen && player.ZoneOverworldHeight && !player.ZoneSkyHeight;

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}