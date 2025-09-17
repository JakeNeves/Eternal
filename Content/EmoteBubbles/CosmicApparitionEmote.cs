using Eternal.Common.Systems;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Eternal.Content.EmoteBubbles
{
    public class CosmicApparitionEmote : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.Dangers);
        }

        public override bool IsUnlocked()
        {
            return DownedBossSystem.downedCosmicApparition;
        }
    }
}