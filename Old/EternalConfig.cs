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
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("Gameplay Tweaks Configuration")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Hell Mode Stat Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool hellModeVanillaBosses = true;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [DefaultValue(false)]
        [BackgroundColor(138, 29, 29)]
        [Label("Use Money Over Apollyon Coins")]
        [Tooltip("The Apollyon NPC uses actual money instead of Apollyon Coins\n(Default: Off)")]
        public bool moneyOverApollyonCoins = false;
    }
}
