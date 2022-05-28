using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Eternal.Common.Systems
{
    public class ZoneSystem : ModPlayer
    {
        public bool zoneComet = false;
        public bool zoneBeneath = false;
        public bool zoneShrine = false;

        public override void ResetEffects()
        {
            zoneComet = false;
            zoneBeneath = false;
            zoneShrine = false;
        }
    }
}
