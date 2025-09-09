using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;

namespace Eternal.Content.Skies
{
    public class CarrionSky : CustomSky
    {
        private bool _isActive;

        private float _fadeOpacity;

        public override void OnLoad()
        {
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _fadeOpacity = 1f / 100f;
            _isActive = true;
            _fadeOpacity = 0.025f;
            _isActive = true;
        }

        public override void Deactivate(params object[] args) => _isActive = false;

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {

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
    }
}
