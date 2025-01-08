using Terraria;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Common.Players
{
    public class BiomeSystem : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            Player.ManageSpecialBiomeVisuals("Eternal:Rift", EventSystem.isRiftOpen);
            Player.ManageSpecialBiomeVisuals("Eternal:PurifiedBeneath", ModContent.GetInstance<ZoneSystem>().zonePurifiedBeneath);

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
