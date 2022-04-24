using Eternal.Items.Weapons.Radiant;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Eternal.UI
{
    internal class EtherealPowerBar : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 490, 1f);
            area.Top.Set(90, 0f);
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.GetTexture("Eternal/UI/EtherealPowerFrame"));
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("0/0", 0.8f);
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(0, 0f);

            gradientA = new Color(62, 34, 153);
            gradientB = new Color(116, 23, 191);

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!(Main.LocalPlayer.HeldItem.modItem is RadiantItem))
                return;

            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<RadiantPlayer>();

            float quotient = (float)modPlayer.etherealPowerCurrent / modPlayer.etherealPowerMax2;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            Rectangle hitBox = barFrame.GetInnerDimensions().ToRectangle();
            hitBox.X += 12;
            hitBox.Width -= 24;
            hitBox.Y += 8;
            hitBox.Height -= 16;

            int left = hitBox.Left;
            int right = hitBox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / (right - left);
                spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitBox.Y, 1, hitBox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!(Main.LocalPlayer.HeldItem.modItem is RadiantItem))
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<RadiantPlayer>();
            text.SetText($"Ethereal Power {modPlayer.etherealPowerCurrent} / {modPlayer.etherealPowerMax2}");
            base.Update(gameTime);
        }
    }
}
