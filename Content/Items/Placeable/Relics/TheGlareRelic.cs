using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Relics
{
    public class TheGlareRelic : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Master;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
            Item.createTile = ModContent.TileType<Tiles.Relics.TheGlareRelic>();
        }
    }
}
