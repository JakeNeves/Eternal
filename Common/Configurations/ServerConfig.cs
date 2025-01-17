﻿using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    // [Label("Eternal Mod Server Configuration")]
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static ServerConfig instance => ModContent.GetInstance<ServerConfig>();

        /// <summary>
        /// Changes some of the base game's recipes as well as add new ones (WIP)
        /// </summary>
        [SeparatePage]
        [Header("GameplayTweaks")]
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.recipeChanges.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.recipeChanges.Tooltip")]
        [ReloadRequired]
        public bool recipeChanges = true;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("HellModeTweaks")]
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.Tooltip")]
        public bool cosmicApparitionNaturalSpawn = true;

        /// <summary>
        /// Makes Hell Mode Cruel
        /// </summary>
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.brutalHellMode.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.brutalHellMode.Tooltip")]
        public bool brutalHellMode = false;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.hellModeVanillaBosses.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.hellModeVanillaBosses.Tooltip")]
        public bool hellModeVanillaBosses = true;

        /// <summary>
        /// EXPERIMENTAL: Adds Life Motes to the game (WIP)
        /// </summary>
        [SeparatePage]
        [Header("ExperimentalFeatures")]
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.lifeMotes.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.lifeMotes.Tooltip")]
        [ReloadRequired]
        public bool lifeMotes = false;

        /// <summary>
        /// EXPERIMENTAL: Adds the Purified Beneath biome (WIP)
        /// </summary>
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.purifiedBeneath.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.purifiedBeneath.Tooltip")]
        [ReloadRequired]
        public bool purifiedBeneath = false;

        /// <summary>
        /// EXPERIMENTAL: Adds some of the upcoming 1.4 content (WIP)
        /// </summary>
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.update14.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.update14.Tooltip")]
        [ReloadRequired]
        public bool update14 = false;
    }
}
