using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;

namespace Eternal.Common.Misc
{
    /// <summary>
    /// A smart HUD state with some code sorced from the Starlight River GitHub Reopsitory
    /// </summary>
    public abstract class SmartHUDState : UIState
    {
        /// <summary>
		/// The UserInterface automatically assigned to this UIState on load.
		/// </summary>
		protected internal virtual UserInterface UserInterface { get; set; }

        /// <summary>
        /// Where this UI state should be inserted relative to the vanilla UI layers.
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="layers">The vanilla UI layers</param>
        /// <returns>The insertion index of this UI state</returns>
        public abstract int InsertionIndex(List<GameInterfaceLayer> layers);

        /// <summary>
        /// If the UI should be visible and interactable or not
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        public virtual bool Visible { get; set; } = false;

        /// <summary>
        /// What scale setting this UI should scale with
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        public virtual InterfaceScaleType Scale { get; set; } = InterfaceScaleType.UI;

        /// <summary>
        /// Allows you to unload anything that might need to be unloaded
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        public virtual void Unload() { }

        internal void AddElement(UIElement element, int x, int y, int width, int height)
        {
            element.Left.Set(x, 0);
            element.Top.Set(y, 0);
            element.Width.Set(width, 0);
            element.Height.Set(height, 0);
            Append(element);
        }

        /// <summary>
        /// Appends an element to another element with the given dimensions
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="element">The element to append</param>
        /// <param name="x">The x position in pixels</param>
        /// <param name="y">The y position in pixels</param>
        /// <param name="width">The width in pixels</param>
        /// <param name="height">The height in pixels</param>
        /// <param name="appendTo">The element to append to</param>
        internal void AddElement(UIElement element, int x, int y, int width, int height, UIElement appendTo)
        {
            element.Left.Set(x, 0);
            element.Top.Set(y, 0);
            element.Width.Set(width, 0);
            element.Height.Set(height, 0);
            appendTo.Append(element);
        }

        /// <summary>
        /// Appends an element to this state with the given dimensions
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="element">The element to append</param>
        /// <param name="x">The x position in pixels</param>
        /// <param name="xPercent">The x position in percentage of the parents width</param>
        /// <param name="y">The y position in pixels</param>
        /// <param name="yPercent">The y position in percentage of the parents height</param>
        /// <param name="width">The width in pixels</param>
        /// <param name="widthPercent">The width in percentage of the parents width</param>
        /// <param name="height">The height in pixels</param>
        /// <param name="heightPercent">The height in percentage of the parents height</param>
        internal void AddElement(UIElement element, int x, float xPercent, int y, float yPercent, int width, float widthPercent, int height, float heightPercent)
        {
            element.Left.Set(x, xPercent);
            element.Top.Set(y, yPercent);
            element.Width.Set(width, widthPercent);
            element.Height.Set(height, heightPercent);
            Append(element);
        }

        /// <summary>
        /// Appends an element to this state with the given dimensions
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="element">The element to append</param>
        /// <param name="x">The x position in pixels</param>
        /// <param name="xPercent">The x position in percentage of the parents width</param>
        /// <param name="y">The y position in pixels</param>
        /// <param name="yPercent">The y position in percentage of the parents height</param>
        /// <param name="width">The width in pixels</param>
        /// <param name="widthPercent">The width in percentage of the parents width</param>
        /// <param name="height">The height in pixels</param>
        /// <param name="heightPercent">The height in percentage of the parents height</param>
        /// <param name="appendTo">The element to append to</param>
        internal void AddElement(UIElement element, int x, float xPercent, int y, float yPercent, int width, float widthPercent, int height, float heightPercent, UIElement appendTo)
        {
            element.Left.Set(x, xPercent);
            element.Top.Set(y, yPercent);
            element.Width.Set(width, widthPercent);
            element.Height.Set(height, heightPercent);
            appendTo.Append(element);
        }

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
