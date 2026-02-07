using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Decorative
{
    public class JakeTEMMemorial : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 52;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 6);
            Item.createTile = ModContent.TileType<Tiles.Decorative.JakeTEMMemorial>();
        }
    }
}
