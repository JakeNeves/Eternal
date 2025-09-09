using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class UndergroundCarrionBackgroundStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/UndergroundCarrionBackground0");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/UndergroundCarrionBackground1");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/UndergroundCarrionBackground2");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/UndergroundCarrionBackground3");
        }
    }
}
