using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Eternal.Common.Players
{
    public static class EternalExt
    {
        /// <summary>
        ///     Recudes code writing to makt life a little easier
        /// </summary>
        /// <param name="plr">Current player</param>
        /// <returns>Returns the PassiveEventSystem player instance</returns>
        public static PassiveEventSystem Eternal(this Player plr)
        {
            return plr.GetModPlayer<PassiveEventSystem>();
        }
    }
}
