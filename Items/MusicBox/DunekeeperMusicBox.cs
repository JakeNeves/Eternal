using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace Eternal.Items.MusicBox
{
    public class DunekeeperMusicBox : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (Dunekeeper)");
            Tooltip.SetDefault("JakeTEM - Dune's Wrath");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.MusicBox.DunekeeperMusicBox>();
            item.rare = ItemRarityID.LightRed;
            item.value = Item.sellPrice(silver: 10);
            item.accessory = true;
        }

    }
}
