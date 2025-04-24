using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Configurations;

namespace Eternal.Content.Items.Materials
{
    public class SpiritRites : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(0, 30, 0, 0);
        }
    }
}
