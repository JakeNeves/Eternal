using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Comet : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscoveryRetake");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneComet = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneComet = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().cometCount >= 60;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
