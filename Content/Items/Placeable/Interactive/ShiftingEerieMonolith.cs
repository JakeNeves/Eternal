using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Placeable.Interactive
{
    public class ShiftingEerieMonolith : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 104;
            Item.maxStack = 9999;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(platinum: 6);
            Item.createTile = ModContent.TileType<Content.Tiles.Interactive.ShiftingEerieMonolith>();
        }
    }
}
