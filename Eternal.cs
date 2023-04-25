using Eternal.Content.Skies;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal
{
	public class Eternal : Mod
	{
        internal static Eternal instance;

        public const string AssetPath = $"{nameof(Eternal)}/Assets/";

        public override void Load()
        {
            instance = this;

            Filters.Scene["Eternal:Rift"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.75f, 0f, 0.75f).UseOpacity(0.75f), EffectPriority.High);
            SkyManager.Instance["Eternal:Rift"] = new RiftSky();
        }

		public override void Unload()
        {
            instance = null;
        }
    }
}