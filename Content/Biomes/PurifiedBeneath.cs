using Eternal.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class PurifiedBeneath : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/GlisteringPasture");

        public override string BestiaryIcon => base.BestiaryIcon;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zonePurifiedBeneath = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zonePurifiedBeneath = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().shinestoneCount >= 36;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
