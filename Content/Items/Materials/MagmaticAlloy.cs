using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class MagmaticAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'A very hot basaltic material'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.maxStack = 9999;
        }
    }
}
