using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    [BackgroundColor(5, 20, 60)]
    [Label("Eternal Mod Client Configuration")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        /// <summary>
        /// Displays a welcome message uppon entering the world
        /// </summary>
        [SeparatePage]
        [Header("Messages")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Show Welcome Message")]
        [Tooltip("Show the welcome message uppon entering the world\n(Default: On)")]
        public bool showWelcomeMessage = true;

        [SeparatePage]
        [Header("QoL Features")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [Label("Boss Defeated Sound")]
        [Tooltip("Plays a sound to notify that a boss has been defeated\n(Default: On)")]
        public bool playDefeatSound = true;
    }
}
