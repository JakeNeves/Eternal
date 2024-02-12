using Eternal.Common.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace Eternal.Content.BossBarStyles
{
    // taken from the example mod, this might be reworked in the future...
    // (This uses a modified version of vanilla's unused bare bones bar)
    public class EternalBossBarStyle : ModBossBarStyle
    {
        public override bool PreventDraw => true;

        public override void Draw(SpriteBatch spriteBatch, IBigProgressBar currentBar, BigProgressBarInfo info)
        {
            if (currentBar == null)
            {
                return;
            }

            if (currentBar is CommonBossBigProgressBar)
            {
                NPC npc = Main.npc[info.npcIndexToAimAt];
                float lifePercent = Utils.Clamp(npc.life / (float)npc.lifeMax, 0f, 1f);

                EternalBossBar.DrawBossBar(spriteBatch, lifePercent);

                if (info.showText && BigProgressBarSystem.ShowText)
                {
                    Rectangle barDimensions = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), new Vector2(400f, 20f));
                    BigProgressBarHelper.DrawHealthText(spriteBatch, barDimensions, 2 * Vector2.UnitY, npc.life, npc.lifeMax);
                }
            }
            else
            {
                currentBar.Draw(ref info, spriteBatch);
            }
        }
    }
}