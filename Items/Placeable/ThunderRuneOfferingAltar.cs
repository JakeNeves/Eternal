using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    public class ThunderRuneOfferingAltar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Tablet Offering Altar");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 38;
            item.maxStack = 99;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 0;
            item.createTile = TileType<Tiles.Interactive.ThunderRuneOfferingAltar>();
        }
    }
}
