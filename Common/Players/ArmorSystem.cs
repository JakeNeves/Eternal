using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class ArmorSystem : ModPlayer
    {
        public static bool StarbornArmor = false;
        public static bool ArkaniumArmor = false;
        public static bool UltimusArmor = false;
        public static bool SubzeroArmor = false;
        public static bool IgneousArmor = false;
        public static bool DuneshockArmor = false;
        public static bool CosmicKeeperArmor = false;
        public static bool IesniumArmor = false;

        public override void ResetEffects()
        {
            StarbornArmor = false;
            ArkaniumArmor = false;
            UltimusArmor = false;
            SubzeroArmor = false;
            IgneousArmor = false;
            DuneshockArmor = false;
            CosmicKeeperArmor = false;
            IesniumArmor = false;
        }
    }
}
