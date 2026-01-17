using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;
using Eternal.Content.NPCs.Boss.Trinity;
using Eternal.Content.Biomes;
using Eternal.Common.Configurations;

namespace Eternal.Common.Players
{
    public class SkySystem : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            Player.ManageSpecialBiomeVisuals("Eternal:Trinity", NPC.AnyNPCs(ModContent.NPCType<TrinityCore>()));

            Player.ManageSpecialBiomeVisuals("Eternal:Rift", EventSystem.isRiftOpen);
            Player.ManageSpecialBiomeVisuals("Eternal:DarkMoon", EventSystem.darkMoon);
            Player.ManageSpecialBiomeVisuals("Eternal:PurifiedBeneath", ModContent.GetInstance<ServerConfig>().purifiedBeneath && Player.InModBiome<PurifiedBeneath>());
            Player.ManageSpecialBiomeVisuals("Eternal:Carrion", Player.InModBiome<CarrionSurface>() || Player.InModBiome<UndergroundCarrion>() || Player.InModBiome<CarrionDesertSurface>());

            // Underworld soul "fog" rift effect
            Player.ManageSpecialBiomeVisuals("Eternal:RiftUnderworldEffect", EventSystem.isRiftOpen && Player.ZoneUnderworldHeight, Player.Center);
            Player.ManageSpecialBiomeVisuals("Eternal:RiftUnderworldEffect2", EventSystem.isRiftOpen && Player.ZoneUnderworldHeight, Player.Center);

            // Sky "storm" rift effect
            Player.ManageSpecialBiomeVisuals("Eternal:RiftSkyEffect", EventSystem.isRiftOpen && Player.ZoneSkyHeight, Player.Center);
            Player.ManageSpecialBiomeVisuals("Eternal:RiftSkyEffect2", EventSystem.isRiftOpen && Player.ZoneSkyHeight, Player.Center);
            Player.ManageSpecialBiomeVisuals("Eternal:RiftSkyEffect3", EventSystem.isRiftOpen && Player.ZoneSkyHeight, Player.Center);
        }
    }
}
