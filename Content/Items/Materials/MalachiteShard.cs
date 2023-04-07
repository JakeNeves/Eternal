using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class MalachiteShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'At least has some value'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.maxStack = 999;
        }
    }
}
