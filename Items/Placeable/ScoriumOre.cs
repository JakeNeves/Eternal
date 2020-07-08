using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class ScoriumOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A Scrap of Scorching Hot Ancient Debris'");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 14;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Red;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = TileType<Tiles.ScoriumOre>();
        }

    }
}
