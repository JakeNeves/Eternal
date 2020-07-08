using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Eternal.Items.Placeable
{
    class LabrynthStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Mined with a Lunar Pickaxe or Higher...");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Green;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = TileType<Tiles.LabrynthStone>();
        }
    }
}
