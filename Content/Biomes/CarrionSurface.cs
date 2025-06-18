using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class CarrionSurface : ModBiome
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.instance.update15;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/NecroticFissure");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<CarrionWaterStyle>();
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<CarrionSurfaceBackgroundStyle>();

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override string MapBackground => BackgroundPath;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneCarrion = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneCarrion = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().carrionBlockCount >= 45;

            bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;

	    bool b3 = player.ZoneSkyHeight || player.ZoneOverworldHeight;

	    return b1 && b2 && b3;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    }
}
