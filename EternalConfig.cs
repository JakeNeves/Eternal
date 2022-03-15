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
        /// Music Configuration to toggle between the new and the old soundtrack
        /// </summary>
        [SeparatePage]
        [Header("Music Configuration")]
        [DefaultValue(false)]
        [Label("Old Eternal Soundtrack")]
        [BackgroundColor(71, 245, 169)]
        [Tooltip("Determines when to use the old soundtrack over the new soundtrack...\n(Default: Off)")]
        public bool originalMusic = false;

        /// <summary>
        /// Music Configuration to toggle between the Eternal Mod and the base game Soundtrack
        /// </summary>
        [DefaultValue(false)]
        [BackgroundColor(71, 245, 169)]
        [Label("Replace Vanilla Music with Eternal Music [c/008060:(WIP)]")]
        [Tooltip("Toggle Between the Vanilla Music and the Eternal Mod Music...\n(Default: Off)")]
        public bool replaceVanillaMusic = false;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("Gameplay Tweaks Configuration")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Hell Mode Stat Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool hellModeVanillaBosses = true;
    }
}
