using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Eternal
{
    /// <summary>
    /// The Eternal Configurations, can be toggled while running/debugging Terraria
    /// </summary>
    [BackgroundColor(5, 20, 60)]
    [Label("Eternal Mod Configuration")]
    public class EternalConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        /// <summary>
        /// Music Configuration to toggle between the new and the classic soundtrack
        /// </summary>
        [SeparatePage]
        [Header("Music Configuration")]
        [DefaultValue(false)]
        [Label("Original Eternal Soundtrack")]
        [BackgroundColor(10, 40, 120)]
        [Tooltip("Determines when to use the classic soundtrack over the new soundtrack...\n(Default: Off)")]
        public bool originalMusic = false;

        /// <summary>
        /// Music Configuration to toggle between the Eternal Mod and the base game Soundtrack
        /// </summary>
        [DefaultValue(true)]
        [BackgroundColor(30, 10, 30)]
        [Label("Replace Vanilla Music with Eternal Music")]
        [Tooltip("Toggle Between the Vanilla Music and the Eternal Mod Music...\n(Default: On)")]
        public bool replaceVanillaMusic = true;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("Gameplay Tweaks Configuration")]
        [DefaultValue(true)]
        [BackgroundColor(60, 20, 5)]
        [Label("Hell Mode Stat Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool hellModeVanillaBosses = true;
    }
}
