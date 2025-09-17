using Eternal.Common.Systems;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Eternal.Content.EmoteBubbles
{
    public class DarkMoonEmote : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.General);
        }

        public override bool IsUnlocked()
        {
            return EventSystem.downedDarkMoon;
        }
    }
}