using Eternal.Common.Systems;
using Eternal.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Mausoleum : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/CrucisNihilo");
        public override int BiomeTorchItemType => ModContent.ItemType<MausoleumTorch>();

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => Color.MediumPurple;
        public override string MapBackground => BackgroundPath;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneMausoleum = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneMausoleum = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().hexedBasaltCount >= 45;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
    }
}
