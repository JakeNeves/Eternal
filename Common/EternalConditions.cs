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

    }
}
