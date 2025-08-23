using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Common
{
    public static class EternalConditions
    {
        // Events
        public static Condition InRift = new("Mods.Eternal.Conditions.InRift", () => EventSystem.isRiftOpen);
        public static Condition DarkMoon = new("Mods.Eternal.Conditions.DarkMoon", () => EventSystem.darkMoon);

        // Biomes
        public static Condition InMausoleum = new("Mods.Eternal.Conditions.InMausoleum", () => Main.LocalPlayer.InModBiome<Mausoleum>());
        public static Condition InGehenna = new("Mods.Eternal.Conditions.InGehenna", () => Main.LocalPlayer.InModBiome<Gehenna>());
        public static Condition InFallenComet = new("Mods.Eternal.Conditions.InFallenComet", () => Main.LocalPlayer.InModBiome<Comet>());
        public static Condition InTheBeneath = new("Mods.Eternal.Conditions.InTheBeneath", () => Main.LocalPlayer.InModBiome<Beneath>());

        // Bosses
        public static Condition IsCarminiteAmalgamationDefeated = new("Mods.Eternal.Conditions.CarmniteAmalgamationDefeated", () => DownedBossSystem.downedCarminiteAmalgamation);
        public static Condition IsDuneGolemDefeated = new("Mods.Eternal.Conditions.DuneGolemDefeated", () => DownedBossSystem.downedDuneGolem);

        public static Condition IsIgneopedeDefeated = new("Mods.Eternal.Conditions.IgneopedeDefeated", () => DownedBossSystem.downedIgneopede);
        public static Condition IsIncineriusDefeated = new("Mods.Eternal.Conditions.IncineriusDefeated", () => DownedBossSystem.downedIncinerius);
        public static Condition IsSubzeroElementalDefeated = new("Mods.Eternal.Conditions.SubzeroElementalDefeated", () => DownedBossSystem.downedSubzeroElemental);
        public static Condition IsNiadesDefeated = new("Mods.Eternal.Conditions.NiadesDefeated", () => DownedBossSystem.downedNiades);
        public static Condition IsGlareDefeated = new("Mods.Eternal.Conditions.GlareDefeated", () => DownedBossSystem.downedGlare);

        public static Condition IsCosmicApparitionDefeated = new("Mods.Eternal.Conditions.CosmicApparitionDefeated", () => DownedBossSystem.downedCosmicApparition);
        public static Condition IsArkofImperiousDefeated = new("Mods.Eternal.Conditions.ArkofImperiousDefeated", () => DownedBossSystem.downedArkofImperious);
        public static Condition IsTrinityDefeated = new("Mods.Eternal.Conditions.TrinityDefeated", () => DownedBossSystem.downedTrinity);
        public static Condition IsCosmicEmperorDefeated = new("Mods.Eternal.Conditions.CosmicEmperorDefeated", () => DownedBossSystem.downedCosmicEmperor);
    }
}
