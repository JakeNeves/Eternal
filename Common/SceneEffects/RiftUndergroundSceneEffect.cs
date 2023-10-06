using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class RiftUndergroundSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/ToTheCenterofTheShiftedWorld");

        public override bool IsSceneEffectActive(Player player) => RiftSystem.isRiftOpen && !player.ZoneOverworldHeight && !player.ZoneUnderworldHeight && !player.ZoneSkyHeight;

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}