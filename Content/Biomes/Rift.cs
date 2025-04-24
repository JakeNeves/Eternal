using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Rift : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AcrossADisfiguredReality");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<RiftWaterStyle>();

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => Color.Magenta;
        public override string MapBackground => BackgroundPath;

        public override bool IsBiomeActive(Player player)
        {
            return EventSystem.isRiftOpen;
        }

        public override void OnInBiome(Player player)
        {
            player.ManageSpecialBiomeVisuals("Eternal:Rift", true);
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
