using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class UnderworldRiftSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AcrossADisfiguredReality");

        public override bool IsSceneEffectActive(Player player) => RiftSystem.isRiftOpen && player.ZoneUnderworldHeight;
    }
}