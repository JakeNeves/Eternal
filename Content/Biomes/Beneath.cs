using Eternal.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Beneath : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DarknessFromDeepBelow");

        public override string BestiaryIcon => base.BestiaryIcon;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneBeneath = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneBeneath = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().grimstoneCount >= 60;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
