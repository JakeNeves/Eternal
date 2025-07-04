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
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.recipeChanges.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.recipeChanges.Tooltip")]
        [ReloadRequired]
        public bool recipeChanges = true;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [SeparatePage]
        [Header("HellModeTweaks")]
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.Tooltip")]
        public bool cosmicApparitionNaturalSpawn = true;

        /// <summary>
        /// Makes Hell Mode Cruel
        /// </summary>
        [DefaultValue(true)]
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.brutalHellMode.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.brutalHellMode.Tooltip")]
        public bool brutalHellMode = false;

        /// <summary>
        /// EXPERIMENTAL: Adds upcoming content from 1.5 to the game
        /// </summary>
        [SeparatePage]
        [Header("ExperimentalFeatures")]
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.update15.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.update15.Tooltip")]
        [ReloadRequired]
        public bool update15 = false;

        /// <summary>
        /// EXPERIMENTAL: Adds Life Motes to the game
        /// </summary>
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.lifeMotes.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.lifeMotes.Tooltip")]
        [ReloadRequired]
        public bool lifeMotes = false;

        /// <summary>
        /// EXPERIMENTAL: Adds the Purified Beneath biome
        /// </summary>
        [DefaultValue(false)]
        [LabelKey("$Mods.Eternal.Server.Configurations.ServerConfig.purifiedBeneath.DisplayName")]
        [TooltipKey("$Mods.Eternal.Server.Configurations.ServerConfig.purifiedBeneath.Tooltip")]
        [ReloadRequired]
        public bool purifiedBeneath = false;
    }
}
