using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Eternal
{
    public class EternalConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false)]
        [Label("Original Eternal Soundtrack")]
        [Tooltip("Determines when to use the original soundtrack over the new soundtrack...\n(Default: Off)")]
        public bool originalMusic = false;

        [DefaultValue(true)]
        [Label("Hell Mode Buffs for Vanilla Bosses")]
        [Tooltip("Determines when to increase the stats of vanilla terraia bosses in hell mode...\n(Default: On)")]
        public bool hellModeVanillaBosses = true;
    }
}
