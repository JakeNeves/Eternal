using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Common.SceneEffects
{
    public class CometNightSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscoveryRetakeNight");

        public override bool IsSceneEffectActive(Player player) => player.InModBiome<Comet>() && !Main.dayTime;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}