using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Eternal.Content.BossBarStyles
{
    public class EternalBossBarStyle : ModBossBarStyle
    {
		public override bool PreventDraw => true;

		public override void Draw(SpriteBatch spriteBatch, IBigProgressBar currentBar, BigProgressBarInfo info)
		{
			
		}
    }
}
