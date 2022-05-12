using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable
{
    public class CometiteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A shard of cosmic debris\n'Pure starpower radiates from this cluster'");
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 12;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.CometiteOre>();
            Item.maxStack = 999;
        }
    }
}
