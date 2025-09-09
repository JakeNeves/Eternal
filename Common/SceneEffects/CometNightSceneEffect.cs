using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class CometNightSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscoveryRetakeNight");

        public override bool IsSceneEffectActive(Player player) => ModContent.GetInstance<ZoneSystem>().zoneComet && !Main.dayTime;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}