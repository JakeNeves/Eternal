using Eternal.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Rarities
{
    public class SinstormMode : ModRarity
    {
        public override Color RarityColor => EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 125 / 125f, [Color.DimGray, Color.Crimson, Color.DimGray]);
    }
}
