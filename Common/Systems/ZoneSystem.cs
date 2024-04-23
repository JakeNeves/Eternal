using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class ZoneSystem : ModPlayer
    {
        public bool zoneComet = false;
        public bool zoneBeneath = false;
        public bool zonePurifiedBeneath = false;
        public bool zoneShrine = false;

        public override void ResetEffects()
        {
            zoneComet = false;
            zoneBeneath = false;
            zonePurifiedBeneath = false;
            zoneShrine = false;
        }
    }
}
