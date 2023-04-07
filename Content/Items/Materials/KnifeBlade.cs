using Eternal.Content.Items.Placeable;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class KnifeBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("???");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 10;
            Item.rare = ItemRarityID.Gray;
        }
    }
}
