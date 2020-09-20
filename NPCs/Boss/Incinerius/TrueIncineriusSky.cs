using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Eternal.NPCs.Boss.Incinerius
{
    public class TrueIncineriusSky : CustomSky
    {

        private bool isActive = false;
        private float opacity = 0f;
        private float intensity = 0f;
        private int TrueIncineriusIndex = -1;

        public override void Update(GameTime gameTime)
        {
            if (Main.gamePaused || !Main.hasFocus)
            {
                return;
            }
            if (isActive && opacity < 0.035f)
            {
                opacity += 0.005f;
            }
            else if (!isActive && opacity > 0f)
            {
                opacity -= 0.005f;
            }
            if (isActive && intensity < 1f)
            {
                opacity += 0.02f;
            }
            else if (!isActive && intensity > 0f)
            {
                opacity -= 0.02f;
            }
        }

        private float GetIntensity()
        {
            if (this.UpdateTrueIncineriusIndex())
            {
                float x = 0f;
                if (this.TrueIncineriusIndex != -1)
                    x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[this.TrueIncineriusIndex].Center);

                return 1f - Utils.SmoothStep(3000f, 6000f, x);
            }
            return 0f;
        }

        public override Color OnTileColor(Color inColor)
        {
            float amt = intensity * .02f;
            return inColor.MultiplyRGB(new Color(175, 75, 255));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (minDepth < 1f || maxDepth == 3.40282347E+38f) {
                var color = new Color(255, 195, 60) * opacity;
                spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
            }
        }

        private bool UpdateTrueIncineriusIndex()
        {
            int TrueIncineriusType = ModLoader.GetMod("Eternal").NPCType("TrueIncinerius");
            if (TrueIncineriusIndex >= 0 && Main.npc[TrueIncineriusIndex].active && Main.npc[TrueIncineriusIndex].type == TrueIncineriusType)
                return true;

            TrueIncineriusIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == TrueIncineriusType)
                {
                    TrueIncineriusIndex = i;
                    break;
                }
            }
            return TrueIncineriusIndex != -1;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            opacity = 0f;
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive;
        }

    }
}
