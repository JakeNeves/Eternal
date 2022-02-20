using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace Eternal.Items.MusicBox
{
    public class BeneathMusicBox : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (The Beneath)");
            if (!ModContent.GetInstance<EternalConfig>().originalMusic)
                Tooltip.SetDefault("JakeTEM - Darkness from Deep Below");
            else
                Tooltip.SetDefault("JakeTEM - Deep Dark");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.MusicBox.BeneathMusicBox>();
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(silver: 10);
            item.accessory = true;
        }

    }
}
