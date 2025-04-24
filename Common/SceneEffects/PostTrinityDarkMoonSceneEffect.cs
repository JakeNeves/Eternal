using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.SceneEffects
{
    public class PostTrinityDarkMoonSceneEffect : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AnethemisOccultae");

        public override bool IsSceneEffectActive(Player player) => DownedBossSystem.downedTrinity && EventSystem.darkMoon && !Main.dayTime;

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }
}