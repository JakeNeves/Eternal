using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Shrine : ModBiome
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Shrine");
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/ImperiousShrine");

        public override string BestiaryIcon => base.BestiaryIcon;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneShrine = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneShrine = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().shrineBrickCount >= 40;

            return b1;
        }
    }
}
