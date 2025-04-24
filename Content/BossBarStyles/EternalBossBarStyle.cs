/*
using Eternal.Common.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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

    public class EternalBossBar
    {
        // Code taken from the base game, there are some slight modifications to this one though... (The code is based off the unused boss bar style.)
        public static void DrawBossBar(SpriteBatch spriteBatch, float lifePercent)
        {
            Rectangle rectangle = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), new Vector2(800f, 10f));
            Rectangle destinationRectangle = rectangle;
            destinationRectangle.Inflate(0, 0);
            Texture2D value = TextureAssets.MagicPixel.Value;
            Rectangle value2 = new Rectangle(0, 0, 1, 1);
            Rectangle destinationRectangle2 = rectangle;
            destinationRectangle2.Width = (int)((float)destinationRectangle2.Width * lifePercent);
            spriteBatch.Draw(value, destinationRectangle, value2, Color.Black * 0.6f);
            spriteBatch.Draw(value, rectangle, value2, Color.Black * 0.6f);
            spriteBatch.Draw(value, destinationRectangle2, value2, Color.Red * 0.5f);
        }
    }
}
*/