using Terraria.ModLoader;

namespace Eternal.Integration
{
    public partial class EternalPlayer : ModPlayer
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public void ResetCalamityModIntegrationEffects()
        {
        }
    }
}
