using Eternal.Common.Systems;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class SnowRift : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AcrossADisfiguredReality");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

        public override string BestiaryIcon => "Assets/Textures/Bestiary/Rift";

        public override bool IsBiomeActive(Player player)
        {
            return RiftSystem.isRiftOpen && player.ZoneSnow;
        }
    }
}
