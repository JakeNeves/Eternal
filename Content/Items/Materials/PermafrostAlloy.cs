using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class PermafrostAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Formed by many blue ice chunks'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 10, silver: 95);
            Item.maxStack = 999;
        }
    }
}
