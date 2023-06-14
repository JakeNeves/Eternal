using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eternal.Common.Misc
{
    /// <summary>
    /// Contains some useful helpers used in the Eternal Mod's code
    /// </summary>
    public static class EternalModHelper
    {
        public static bool Contains(this Rectangle rect, Vector2 pos) => rect.Contains((int)pos.X, (int)pos.Y);
    }
}
