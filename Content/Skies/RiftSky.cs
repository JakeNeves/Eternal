using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Eternal.Content.Skies
{
    public class RiftSky : CustomSky
    {
        private RiftSky.Star[] _stars;
        private Texture2D _bgTexture;
        private Texture2D[] _starTextures;

        private UnifiedRandom _random = new UnifiedRandom();

        private bool _isActive;

        private float _fadeOpacity;

        public override void OnLoad()
        {
            _bgTexture = ModContent.Request<Texture2D>("Terraria/Images/Misc/NebulaSky/Background", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            _starTextures = new Texture2D[2];
            for (int index = 0; index < _starTextures.Length; ++index)
                _starTextures[index] = ModContent.Request<Texture2D>("Terraria/Images/Misc/StarDustSky/Star " + index, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _fadeOpacity = 1f / 500f;
            _isActive = true;
            int num1 = 200;
            int num2 = 10;
            _stars = new RiftSky.Star[num1 * num2];
            int index1 = 0;
            for (int index2 = 0; index2 < num1; ++index2)
            {
                float num3 = index2 / num1;
                for (int index3 = 0; index3 < num2; ++index3)
                {
                    float num4 = index3 / num2;
                    _stars[index1].Position.X = (float)((double)num3 * Main.maxTilesX * 16.0);
                    _stars[index1].Position.Y = (float)((double)num4 * (Main.worldSurface * 16.0 + 2000.0) - 1000.0);
                    _stars[index1].Depth = (float)((double)_random.NextFloat() * 8.0 + 1.5);
                    _stars[index1].TextureIndex = _random.Next(_starTextures.Length);
                    _stars[index1].SinOffset = _random.NextFloat() * 6.28f;
                    _stars[index1].AlphaAmplitude = _random.NextFloat() * 5f;
                    _stars[index1].AlphaFrequency = _random.NextFloat() + 1f;
                    ++index1;
                }
            }
            Array.Sort<RiftSky.Star>(_stars, new Comparison<RiftSky.Star>(SortMethod));

            _fadeOpacity = 0.002f;
            _isActive = true;
        }

        public override void Deactivate(params object[] args) => _isActive = false;

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            _fadeOpacity = 1f / 500f;
            _isActive = true;
            int num1 = -1;
            int num2 = 0;
            for (int index = 0; index < _stars.Length; ++index)
            {
                float depth = _stars[index].Depth;
                if (num1 == -1 && (double)depth < (double)maxDepth)
                    num1 = index;
                if ((double)depth > (double)minDepth)
                    num2 = index;
                else
                    break;
            }
            if (num1 == -1)
                return;
            float num3 = Math.Min(1f, (float)((Main.screenPosition.Y - 1000.0) / 1000.0));
            Vector2 vector2_3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int index = num1; index < num2; ++index)
            {
                Vector2 vector2_4 = new Vector2(1f / _stars[index].Depth, 1.1f / _stars[index].Depth);
                Vector2 position = (_stars[index].Position - vector2_3) * vector2_4 + vector2_3 - Main.screenPosition;
                if (rectangle.Contains((int)position.X, (int)position.Y))
                {
                    float num4 = (float)Math.Sin(_stars[index].AlphaFrequency * (double)Main.GlobalTimeWrappedHourly + _stars[index].SinOffset) * _stars[index].AlphaAmplitude + _stars[index].AlphaAmplitude;
                    float num5 = (float)(Math.Sin(_stars[index].AlphaFrequency * (double)Main.GlobalTimeWrappedHourly * 5.0 + _stars[index].SinOffset) * 0.10000000149011612 - 0.10000000149011612);
                    float num6 = MathHelper.Clamp(num4, 0.0f, 1f);
                    Texture2D texture = _starTextures[_stars[index].TextureIndex];
                    spriteBatch.Draw(texture, position, new Rectangle?(), Color.White * num3 * num6 * 0.8f * (1f - num5) * _fadeOpacity, 0.0f, new Vector2((float)(texture.Width >> 1), (float)(texture.Height >> 1)), (float)(((double)vector2_4.X * 0.5 + 0.5) * ((double)num6 * 0.30000001192092896 + 0.699999988079071)), SpriteEffects.None, 0.0f);
                }
            }
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(_bgTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 1500.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), new Color(94, 255, 250, 240) * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * _fadeOpacity));
                Vector2 value = new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
                Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
            }
        }

        public override bool IsActive() => _isActive || _fadeOpacity > 1.0 / 1000.0;

        public override void Reset() => _isActive = false;

        public override void Update(GameTime gameTime)
        {
            if (_isActive && _fadeOpacity < 1f)
            {
                _fadeOpacity += 0.1f;
            }
            else if (!_isActive && _fadeOpacity > 0f)
            {
                _fadeOpacity -= 0.05f;
            }
        }

        private int SortMethod(RiftSky.Star meteor1, RiftSky.Star meteor2) => meteor2.Depth.CompareTo(meteor1.Depth);

        private struct Star
        {
            public Vector2 Position;
            public float Depth;
            public int TextureIndex;
            public float SinOffset;
            public float AlphaFrequency;
            public float AlphaAmplitude;
        }
    }
}
