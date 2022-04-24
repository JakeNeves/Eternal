using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalGlowmask : ModPlayer
    {
        private static readonly Dictionary<int, Texture2D> ItemGlowMask = new Dictionary<int, Texture2D>();

        internal static void Unload()
        {
            ItemGlowMask.Clear();
        }

        /// <summary>
        ///     Applies The Glowmask to the Item
        /// </summary>
        public static void AddGlowMask(int itemType, string texturePath)
        {
            ItemGlowMask[itemType] = ModContent.GetTexture(texturePath);
        }
    }
}
