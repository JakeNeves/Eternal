using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class DarkMoon : ModBiome
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DarkTwistedNightmare");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override string BestiaryIcon => base.BestiaryIcon;

        public override bool IsBiomeActive(Player player)
        {
            return EventSystem.darkMoon && !Main.dayTime;
        }

        public override void OnInBiome(Player player)
        {
            if (Main.dayTime)
                EventSystem.darkMoon = false;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
