using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.CraftingStations
{
    public class Starforge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft starmetal and cometite-teir items");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 48;
            Item.maxStack = 999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(gold: 20);
            Item.createTile = ModContent.TileType<Content.Tiles.CraftingStations.Starforge>();
        }
    }
}
