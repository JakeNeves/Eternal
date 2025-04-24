using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Decorative
{
    public class GehennaSpike : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Decorative.GehennaSpike>();
            Item.maxStack = 9999;
        }
    }
}
