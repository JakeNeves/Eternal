using Eternal.Common.GlobalProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Eternal.Common.Misc
{
    public static class EternalExtension
    {
        public static WeaponGlobalProjectile Eternal(this Projectile proj) => proj.GetGlobalProjectile<WeaponGlobalProjectile>();
    }
}
