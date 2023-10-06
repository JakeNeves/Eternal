using Eternal.Common.Systems;
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

        public override bool IsBiomeActive(Player player)
        {
            return RiftSystem.isRiftOpen;
        }

        public override void OnInBiome(Player player)
        {
            player.ManageSpecialBiomeVisuals("Eternal:Rift", true);
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
