using Microsoft.Xna.Framework;
using Terraria;

namespace Eternal.Common
{
    public class EternalCommonUtils
    {

        /// <summary>
        /// Allows you to do some pretty cool things with lerping the color values.
        /// </summary>
        /// <param name="percent">The percentage</param>
        /// <param name="colors">The Colors</param>
        /// <returns></returns>
        public static Color MultiLerpColor(float percent, params Color[] colors)
        {
            float per = 1f / ((float)colors.Length - 1);
            float total = per;
            int currentID = 0;
            while (percent / total > 1f && currentID < colors.Length - 2) { total += per; currentID++; }
            return Color.Lerp(colors[currentID], colors[currentID + 1], (percent - per * currentID) / per);
        }

        // from the spirit mod repo, i just changed the summary desc.
        /// <summary>
		/// Checks if the given area is flat or not.
		/// Returns false if the average tile height variation is greater than the threshold.
		/// Expects that the first tile is solid, and continues from there.
		/// Use the weight parameters to change the importance of up/down checks.
		/// </summary>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="width"></param>
		/// <param name="threshold"></param>
		/// <param name="goingDownWeight"></param>
		/// <param name="goingUpWeight"></param>
		/// <returns></returns>
		public static bool CheckFlat(int startX, int startY, int width, float threshold, int goingDownWeight = 0, int goingUpWeight = 0)
        {
            if (!WorldGen.SolidTile(startX + width, startY)) return false;

            float totalVariance = 0;
            for (int i = 0; i < width; i++)
            {
                if (startX + i >= Main.maxTilesX) return false;

                for (int k = startY - 1; k > startY - 100; k--)
                {
                    if (WorldGen.SolidTile(startX + i, k)) return false;
                }

                int offset = 0;
                bool goingUp = WorldGen.SolidTile(startX + i, startY);
                offset += goingUp ? goingUpWeight : goingDownWeight;
                while ((goingUp && WorldGen.SolidTile(startX + i, startY - offset))
                    || (!goingUp && !WorldGen.SolidTile(startX + i, startY + offset)))
                {
                    offset++;
                }
                if (goingUp) offset--;
                totalVariance += offset;
            }
            return totalVariance / width <= threshold;
        }

    }
}
