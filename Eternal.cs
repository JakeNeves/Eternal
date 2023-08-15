using Eternal.Content.Items.Misc;
using Eternal.Content.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Eternal
{
	public class Eternal : Mod
	{
        internal static Eternal instance;

        public static int Naquadarinite;

        public const string AssetPath = $"{nameof(Eternal)}/Assets/";

        public override void Load()
        {
            instance = this;

            if (!Main.dedServ)
            {
                // GameShaders.Misc["Eternal:ApparitionalParticle"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>("Assets/Effects/ApparitionalParticle").Value), "ApparitionalParticle"));

                Filters.Scene["Eternal:Rift"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.75f, 0f, 0.75f).UseOpacity(0.5f), EffectPriority.High);
                SkyManager.Instance["Eternal:Rift"] = new RiftSky();

                Filters.Scene["Eternal:RiftUnderworldEffect"] = new Filter((new BlizzardShaderData("FilterBlizzardForeground")).UseColor(0.5f, 0.0f, 0.5f).UseSecondaryColor(0.25f, 0.0f, 0.25f).UseImage("Images/Misc/Perlin", 0, null).UseOpacity(0.030f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Filters.Scene["Eternal:RiftUnderworldEffect2"] = new Filter((new SandstormShaderData("FilterSandstormForeground")).UseColor(0.5f, 0.0f, 0.5f).UseSecondaryColor(0.25f, 0.0f, 0.25f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.15f).UseImageScale(new Vector2(-3f, -0.75f), 0), EffectPriority.High);

                Filters.Scene["Eternal:RiftSkyEffect"] = new Filter((new SandstormShaderData("FilterSandstormForeground")).UseColor(0.025f, 0.0f, 0.025f).UseImage(ModContent.Request<Texture2D>("Eternal/Assets/Textures/Misc/MilkyNoise", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, 0, null).UseOpacity(0.015f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Filters.Scene["Eternal:RiftSkyEffect2"] = new Filter((new SandstormShaderData("FilterSandstormForeground")).UseColor(0.025f, 0.0f, 0.025f).UseImage(ModContent.Request<Texture2D>("Eternal/Assets/Textures/Misc/MilkyNoise2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, 0, null).UseOpacity(0.010f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Filters.Scene["Eternal:RiftSkyEffect3"] = new Filter((new SandstormShaderData("FilterSandstormForeground")).UseColor(0.025f, 0.0f, 0.025f).UseImage(ModContent.Request<Texture2D>("Eternal/Assets/Textures/Misc/MilkyNoise3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, 0, null).UseOpacity(0.005f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
            }

            Naquadarinite = CustomCurrencyManager.RegisterCurrency(new Naquadarinite(ModContent.ItemType<NaquadariniteMote>(), 999L, "Naquadarinite"));
        }

		public override void Unload()
        {
            instance = null;
        }
    }
}