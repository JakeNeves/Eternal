using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Eternal.Skies
{
    public class AshpitSky : CustomSky
    {
        private struct LightPillar
        {
            public Vector2 Position;
            public float Depth;
        }

        private LightPillar[] _pillars;
        private Texture2D _bgTexture;
        private UnifiedRandom _random = new UnifiedRandom();

        private bool skyActive;
        private float opacity;

        public override void OnLoad()
        {
            this._bgTexture = TextureManager.Load("Images/Misc/SolarSky/Background");
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            opacity = 0.002f;
            skyActive = true;
            _pillars = new LightPillar[40];
            for (int i = 0; i < _pillars.Length; i++)
            {
                _pillars[i].Position.X = (float)i / _pillars.Length * (Main.maxTilesX * 16f + 20000f) + _random.NextFloat() * 40f - 20f - 20000f;
                _pillars[i].Position.Y = _random.NextFloat() * 200f - 800f;
                _pillars[i].Depth = _random.NextFloat() * 8f + 7f;
            }
            Array.Sort(_pillars, SortMethod);
        }

        public override void Deactivate(params object[] args) => skyActive = false;

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            int num11 = -1;
            int num10 = 0;
            for (int j = 0; j < _pillars.Length; j++)
            {
                float depth = _pillars[j].Depth;
                if (num11 == -1 && depth < maxDepth)
                {
                    num11 = j;
                }
                if(depth <= minDepth)
                {
                    break;
                }
                num10 = j;
            }
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(this._bgTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 1500.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), new Color(94, 255, 250, 240) * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * opacity));
                Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
                Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
            }
        }

        public override bool IsActive() => skyActive || opacity > 0.001f;

        public override void Reset() => skyActive = false;

        public override void Update(GameTime gameTime)
        {
            if (skyActive && opacity < 1f)
            {
                opacity += 0.1f;
            }
            else if (!skyActive && opacity > 0f)
            {
                opacity -= 0.05f;
            }
        }

        private int SortMethod(LightPillar pillar1, LightPillar pillar2) => pillar2.Depth.CompareTo(pillar1.Depth);
    }
}
