using Microsoft.Xna.Framework;
using Terraria;

namespace Eternal
{
    public static class EternalUtils
    {
        public static EternalPlayer GetEternalPlayer(this Player player) => player.GetModPlayer<EternalPlayer>();
    }
}
