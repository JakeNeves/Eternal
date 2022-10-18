using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace Eternal.Common.Lists
{
    public static class ProjectileLists
    {
        #region projLists
        public static List<int> canUseMeleeFunctionality = new()
        {
            ProjectileID.BlandWhip,
            ProjectileID.BoneWhip,
            ProjectileID.CoolWhip,
            ProjectileID.FireWhip,
            ProjectileID.RainbowWhip,
            ProjectileID.MaceWhip,
            ProjectileID.ScytheWhip,
            ProjectileID.SwordWhip,
            ProjectileID.ThornWhip
        };
        #endregion
    }
}
