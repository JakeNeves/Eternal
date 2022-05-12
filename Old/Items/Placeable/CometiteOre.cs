using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Placeable
{
    class CometiteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A shard of cosmic debris\n'Pure starpower radiates from this cluster'");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 12;
            item.value = Item.sellPrice(gold: 50);
            item.rare = ItemRarityID.Red;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = TileType<Tiles.CometiteOre>();
            item.maxStack = 999;
        }
    }
}
