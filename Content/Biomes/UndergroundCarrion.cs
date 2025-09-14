using Eternal.Common.Systems;
using Eternal.Content.Items.Placeable;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class UndergroundCarrion : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/NecroventricHeap");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Corrupt;
        public override int BiomeTorchItemType => ModContent.ItemType<CarrionTorch>();

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<CarrionWaterStyle>();
        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<UndergroundCarrionBackgroundStyle>();

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override string MapBackground => BackgroundPath;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneUndergroundCarrion = false;
        }

        public override void OnInBiome(Player player)
        {
            player.ManageSpecialBiomeVisuals("Eternal:Carrion", true);
        }

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) &&
                ModContent.GetInstance<BiomeTileCount>().carrionBlockCount >= 96 &&
                Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
}
