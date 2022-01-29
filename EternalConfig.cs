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

        [SeparatePage]
        [Header("Music Configuration")]
        [DefaultValue(false)]
        [Label("Original Eternal Soundtrack")]
        [BackgroundColor(10, 40, 120)]
        [Tooltip("Determines when to use the original soundtrack over the new soundtrack...\n(Default: Off)")]
        public bool originalMusic = false;

        [DefaultValue(true)]
        [BackgroundColor(30, 10, 30)]
        [Label("Replace Vanilla Music with Eternal Music")]
        [Tooltip("Toggle Between the Vanilla Music and the Eternal Mod Music...\n(Default: On)")]
        public bool replaceVanillaMusic = true;

        [SeparatePage]
        [Header("Gameplay Tweaks Configuration")]
        [DefaultValue(true)]
        [BackgroundColor(60, 20, 5)]
        [Label("Hell Mode Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool hellModeVanillaBosses = true;
    }
}
