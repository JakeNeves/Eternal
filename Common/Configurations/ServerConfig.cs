using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Eternal.Common.Configurations
{
    [BackgroundColor(5, 20, 60)]
    // [Label("Eternal Mod Server Configuration")]
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        /// <summary>
        /// Changes some of the base game's recipes as well as add new ones
        /// </summary>
        [SeparatePage]
        [Header("GameplayTweaks")]
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
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
        [BackgroundColor(138, 29, 29)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.cosmicApparitionNaturalSpawn.Tooltip")]
        public bool cosmicApparitionNaturalSpawn = true;

        /// <summary>
        /// Makes Hell Mode Cruel
        /// </summary>
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.brutalHellMode.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.brutalHellMode.Tooltip")]
        public bool brutalHellMode = false;

        /// <summary>
        /// Gives bosses from the base game an increase of stats, simular to Master Mode
        /// </summary>
        [DefaultValue(true)]
        [BackgroundColor(138, 29, 29)]
        [LabelKey("$Mods.Eternal.Common.Configurations.ServerConfig.hellModeVanillaBosses.DisplayName")]
        [TooltipKey("$Mods.Eternal.Common.Configurations.ServerConfig.hellModeVanillaBosses.Tooltip")]
        public bool hellModeVanillaBosses = true;
    }
}
