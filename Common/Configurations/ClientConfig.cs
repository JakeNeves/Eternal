﻿using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    // [Label("Eternal Mod Client Configuration")]
    public class ClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static ClientConfig instance => ModContent.GetInstance<ClientConfig>();

        /// <summary>
        /// Displays a welcome message uppon entering the world
        /// </summary>
        [SeparatePage]
        [Header("Messages")]
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Client.Configurations.ClientConfig.showWelcomeMessage.DisplayName")]
        [TooltipKey("$Mods.Eternal.Client.Configurations.ClientConfig.showWelcomeMessage.Tooltip")]
        public bool showWelcomeMessage = true;

        [SeparatePage]
        [Header("EternalModBossBarSettings")]
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Client.Configurations.ClientConfig.bossBarExtras.DisplayName")]
        [TooltipKey("$Mods.Eternal.Client.Configurations.ClientConfig.bossBarExtras.Tooltip")]
        public bool bossBarExtras = false;

        [SeparatePage]
        [Header("ExperimentalFeatures")]
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Client.Configurations.ClientConfig.playDefeatSound.DisplayName")]
        [TooltipKey("$Mods.Eternal.Client.Configurations.ClientConfig.playDefeatSound.Tooltip")]
        public bool playDefeatSound = false;
    }
}
