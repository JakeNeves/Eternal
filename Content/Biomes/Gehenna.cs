using Eternal.Common.Systems;
using Eternal.Content.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace Eternal.Content.Biomes
{
    public class Gehenna : ModBiome
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Tartarus");
        public override int BiomeTorchItemType => ModContent.ItemType<GehennaTorch>();

        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => Color.DarkRed;
        public override string MapBackground => BackgroundPath;

        public override void OnEnter(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneGehenna = true;
        }

        public override void OnLeave(Player player)
        {
            ModContent.GetInstance<ZoneSystem>().zoneGehenna = false;
        }

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = ModContent.GetInstance<BiomeTileCount>().redBasaltCount >= 45;

            return b1;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    }
}
