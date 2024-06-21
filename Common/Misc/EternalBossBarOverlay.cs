using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Eternal.Common.Misc
{
    /// <summary>
    /// A feature added by Eternal to add the boss name above the vanilla boss bar as was as a subtitle above the boss name.
    /// (Other mods may not support the subtitle feature)
    /// </summary>
    public class EternalBossBarOverlay : SmartHUDState
    {
        public static bool visible;

        public static NPC tracked;
        public static string text;
        public static Texture2D texture = ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public static Color glowColor = Color.Transparent;

        private readonly BarOverlay bar = new();

        public override bool Visible => visible;

        public float Priority => 1f;

        public override int InsertionIndex(List<GameInterfaceLayer> layers)
        {
            return layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        }

        public override void OnInitialize()
        {
            bar.Left.Set(-258, 0.5f);
            bar.Top.Set(-76, 1);
            bar.Width.Set(516, 0);
            bar.Height.Set(46, 0);
            Append(bar);
        }

        public override void SafeUpdate(GameTime gameTime)
        {
            Recalculate();

            if (tracked is null)
                visible = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Used for the Eternal Mod boss bar style
        /// </summary>
        /// <param name="text">The text that appears above the NPC's name</param>
        /// <param name="NPC"></param>
        public static void SetTracked(string text, NPC NPC)
        {
            tracked = NPC;
            EternalBossBarOverlay.text = text;
            visible = true;
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

    public class BarOverlay : SmartHUDElement
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            NPC NPC = EternalBossBarOverlay.tracked;

            if (NPC is null)
                return;

            Vector2 pos = GetDimensions().ToRectangle().TopLeft() + new Vector2(0, 1);
            var off = new Vector2(30, 12);

            if (NPC is null || !NPC.active || !Main.LocalPlayer.active)
            {
                EternalBossBarOverlay.visible = false;
                return;
            }

            int progress = (int)(EternalBossBarOverlay.tracked?.life / (float)EternalBossBarOverlay.tracked?.lifeMax * 456);

            Utils.DrawBorderString(spriteBatch, EternalBossBarOverlay.text, pos + new Vector2(516 / 2, -40), Color.White, 0.75f, 0.5f, 0);
            Utils.DrawBorderString(spriteBatch, NPC.FullName, pos + new Vector2(516 / 2, -25), Color.White, 1.5f, 0.5f, 0);
        }
    }
}
