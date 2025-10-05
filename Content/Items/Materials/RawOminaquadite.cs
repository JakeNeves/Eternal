using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Terraria.ID;

namespace Eternal.Content.Items.Materials
{
    public class RawOminaquadite : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<OminaquaditeBar>();
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<Maroon>();
            Item.value = Item.sellPrice(platinum: 10);
            Item.maxStack = 9999;
        }
    }
}
