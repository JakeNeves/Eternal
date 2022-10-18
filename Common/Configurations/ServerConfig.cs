using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    [BackgroundColor(5, 20, 60)]
    [Label("Eternal Mod Server Configuration")]
    public class ServerConfig : ModConfig
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

        /// <summary>
        /// Makes Hell Mode Cruel
        /// </summary>
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Brutal Hell (Hell Mode+) Augmentations")]
        [Tooltip("Makes Hell Mode Cruel. Adds a temperature system, makes the night extremely dark, etc.\n(Default: Off)")]
        public bool BrutalHellMode = false;

        /// <summary>
        /// Allows you to obtain crazy rare drops from bosses in expert, master and hell mode
        /// </summary>
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Absolute RNG Drops")]
        [Tooltip("Gives you a chance to obtain [c/F72F9A:crazy rare] loot from bosses. [c/ED0249:This only applies to expert and master mode, hell mode has unique items]\n(Default: On)")]
        public bool absoluteRNGDrops = true;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("Hell Mode Tweaks")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Naturally Spawning cosmic Apparition")]
        [Tooltip("Makes the Cosmic Apparition spawn naturally when it's not defeated in the world yet\n(Default: On)")]
        public bool cosmicApparitionNaturalSpawn = true;
    }
}
