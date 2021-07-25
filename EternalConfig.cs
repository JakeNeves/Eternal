using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Eternal
{
    public class EternalConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false)]
        [Label("Original Eternal Soundtrack")]
        [Tooltip("Determines when to use the original soundtrack over the new soundtrack...\n(Default: off)")]
        public bool originalMusic = false;
    }
}
