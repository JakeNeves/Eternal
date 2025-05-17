using Microsoft.Xna.Framework;

namespace Eternal.Common
{
    public class EternalCommonUtils
    {

        /// <summary>
        /// Allows you to do some pretty cool things with lerping the color values.
        /// </summary>
        /// <param name="percent">The percentage at witch the color fade rate is</param>
        /// <param name="colors">The Colors given within the parameters</param>
        /// <returns></returns>
        public static Color MultiLerpColor(float percent, params Color[] colors)
        {
            float per = 1f / ((float)colors.Length - 1);
            float total = per;
            int currentID = 0;
            while (percent / total > 1f && currentID < colors.Length - 2) { total += per; currentID++; }
            return Color.Lerp(colors[currentID], colors[currentID + 1], (percent - per * currentID) / per);
        }
    }
}
