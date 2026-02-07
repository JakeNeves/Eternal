using Eternal.Content.DamageClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Eternal.Common
{
    public class EternalCommonUtils
    {
        public Item item;

        /// <summary>
        /// Determines if a given NPC is excluded from dropping Essences (Essence of Light/Night/Blight)
        /// <br/>This works similarly to <see cref="NPCID.Sets.CannotDropSouls"/>
        /// </summary>
        public static bool[] CannotDropEssences = NPCID.Sets.Factory.CreateBoolSet(NPCID.BlueSlime, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Slimer, NPCID.SlimeSpiked);
       
        /// <summary>
        /// Allows you to change whether or not a weapon receives radiant prefixes. Return true if the item should receive radiant prefixes and false if it should not.
        /// </summary>
        public bool RadiantPrefix() => item.DamageType.GetsPrefixesFor<Radiant>();

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
