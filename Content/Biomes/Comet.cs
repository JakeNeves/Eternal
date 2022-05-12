using Eternal.Common.Systems;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Comet : ModBiome
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Comet");
        }

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Mushroom;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscovery");

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().cometCount >= 30;

            return b1;
        }
    }
}
