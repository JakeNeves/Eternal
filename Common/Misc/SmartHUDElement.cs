using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Eternal.Common.Misc
{
    /// <summary>
    /// A smart HUD element with some code sorced from the Starlight River GitHub Reopsitory
    /// (This is primarly used for Eternal's boss bar features)
    /// </summary>
    public abstract class SmartHUDElement : UIElement
    {
        /// <summary>
		/// A Safe wrapper around Update that allows both an override and the OnUpdate event to be used together
        /// (Sourced from the Starlight River GitHub Repository)
		/// </summary>
		/// <param name="evt">The mouse event that occured to fire this listener</param>
		public virtual void SafeUpdate(GameTime gameTime) { }

        public sealed override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SafeUpdate(gameTime);
        }
    }
}
