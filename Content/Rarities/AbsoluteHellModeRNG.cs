using Eternal.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class AbsoluteHellModeRNG : ModRarity
    {
        public override Color RarityColor => EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkRed, Color.DeepPink, Color.DarkRed);
    }
}
