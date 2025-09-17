using Eternal.Common.Systems;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Eternal.Content.EmoteBubbles
{
    public class HeriticEmote : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.Town);
        }

        public override bool IsUnlocked()
        {
            return DownedBossSystem.downedNiades;
        }
    }
}