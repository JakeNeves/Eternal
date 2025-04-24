using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class DarkMoon : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DarkTwistedNightmare");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => Color.DarkViolet;
        public override string MapBackground => BackgroundPath;

        public override bool IsBiomeActive(Player player)
        {
            return EventSystem.darkMoon && !Main.dayTime;
        }

        public override void OnInBiome(Player player)
        {
            player.ManageSpecialBiomeVisuals("Eternal:DarkMoon", true);
        }

        public override void OnLeave(Player player)
        {
            if (!EventSystem.downedDarkMoon)
                EventSystem.downedDarkMoon = true;

            if (DownedBossSystem.downedTrinity && EventSystem.downedDarkMoon && !EventSystem.downedDarkMoon2)
                EventSystem.downedDarkMoon2 = true;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
