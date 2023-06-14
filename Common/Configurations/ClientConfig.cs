using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    [BackgroundColor(5, 20, 60)]
    // [Label("Eternal Mod Client Configuration")]
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
        [LabelKey("$Mods.Eternal.Common.Configurations.ClientConfig.showWelcomeMessage.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ClientConfig.showWelcomeMessage.Tooltip")]
        public bool showWelcomeMessage = true;

        [SeparatePage]
        [Header("QoLFeatures")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ClientConfig.playDefeatSound.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ClientConfig.playDefeatSound.Tooltip")]
        public bool playDefeatSound = false;
    }
}
