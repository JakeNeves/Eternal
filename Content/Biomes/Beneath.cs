using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Beneath : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/PsionicEchoes");

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath;

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
            bool b1 = ModContent.GetInstance<BiomeTileCount>().gloomrockCount >= 60;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
