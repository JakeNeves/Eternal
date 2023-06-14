using Eternal.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class HellMode : ModRarity
    {
        public override Color RarityColor => EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(140, 30, 30), Color.PaleVioletRed, new Color(140, 30, 30));
    }
}
