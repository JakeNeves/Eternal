using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    [BackgroundColor(5, 20, 60)]
    [Label("Eternal Mod Common Configuration")]
    public class CommonConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("Gameplay Tweaks")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Hell Mode Stat Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool HellModeVanillaBosses = true;
    }
}
