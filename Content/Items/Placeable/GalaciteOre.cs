using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Placeable
{
    public class GalaciteOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragment of frozen energy\n'Cold to the touch'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ModContent.RarityType<Teal>();
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.GalaciteOre>();
            Item.maxStack = 999;
        }
    }
}
