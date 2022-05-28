using Eternal.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Comet : ModBiome
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Comet");
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/AstralDiscovery");

        public override string BestiaryIcon => "Textures/Bestiary/Comet";

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
            bool b1 = ModContent.GetInstance<BiomeTileCount>().cometCount >= 30;

            return b1;
        }
    }
}
