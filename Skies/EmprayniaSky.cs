using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Eternal.Skies
{
    public class EmprayniaSky : CustomSky
    {
        private bool isActive = false;
        private float opacity = 0;
        private int EmprayniaIndex = -1;

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(2.29f, 0.84f, 2.55f) * opacity);
            }
            
            if (maxDepth >= 0 && minDepth < 0)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(2.29f, 0.84f, 2.55f) * 0.5f);
            }
        }

        public override bool IsActive()
        {
            return isActive || opacity > 0f;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive && opacity < 1f)
            {
                opacity += 0.02f;
            }
            else if (!isActive && opacity > 0f)
            {
                opacity -= 0.02f;
            }
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        private float GetOpacity()
        {
            if(this.UpdateEmprayniaIndex())
            {
                float x = 0f;
                if (this.EmprayniaIndex != -1)
                    x = Vector2.Distance(Main.player[Main.myPlayer].Center, Main.npc[this.EmprayniaIndex].Center);

                return 1f - Utils.SmoothStep(3000f, 6000f, x);
            }
            return 0f;
        }

        private bool UpdateEmprayniaIndex()
        {
            int EmprayniaType = ModLoader.GetMod("Eternal").NPCType("Empraynia");
            if (EmprayniaIndex >= 0 && Main.npc[EmprayniaIndex].active && Main.npc[EmprayniaIndex].type == EmprayniaType)
                return true;

            EmprayniaIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == EmprayniaType)
                {
                    EmprayniaIndex = i;
                    break;
                }
            }

            return EmprayniaIndex != -1;
        }

        public override Color OnTileColor(Color inColor)
        {
            float amt = opacity * .02f;
            return inColor.MultiplyRGB(Color.Purple /*new Color(0.229f - amt, 0.84f - amt, 0.255f - amt)*/);
        }
    }
}
