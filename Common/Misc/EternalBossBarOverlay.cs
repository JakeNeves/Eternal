using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;

namespace Eternal.Common.Misc
{
    public class EternalBossBarOverlay : SmartHUDState
    {
        public static bool visible;

        public static NPC tracked;
        public static string text;
        public static Texture2D texture = ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public static Color glowColor = Color.Transparent;

        public static bool? forceInvulnerabilityVisuals = null;

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

        public static void SetTracked(string text, NPC NPC, Texture2D tex = default)
        {
            tracked = NPC;
            EternalBossBarOverlay.text = text;
            visible = true;

            if (tex != default)
                texture = tex;
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
                EternalBossBarOverlay.forceInvulnerabilityVisuals = null;
                return;
            }

            Texture2D texGlow = ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossbarGlow").Value;

            int progress = (int)(EternalBossBarOverlay.tracked?.life / (float)EternalBossBarOverlay.tracked?.lifeMax * 456);

            spriteBatch.End();
            spriteBatch.Begin(default, BlendState.Additive, default, default, default, default, Main.UIScaleMatrix);

            spriteBatch.Draw(texGlow, pos + off, EternalBossBarOverlay.glowColor * 0.5f);
            spriteBatch.Draw(texGlow, new Rectangle((int)(pos.X + off.X), (int)(pos.Y + off.Y), progress, 22), new Rectangle(0, 0, progress, 22), EternalBossBarOverlay.glowColor);

            spriteBatch.End();
            spriteBatch.Begin(default, default, default, default, default, default, Main.UIScaleMatrix);

            Utils.DrawBorderString(spriteBatch, EternalBossBarOverlay.text + NPC.FullName + ": " + NPC.life + "/" + NPC.lifeMax, pos + new Vector2(516 / 2, -20), Color.White, 1, 0.5f, 0);
        }
    }
}
