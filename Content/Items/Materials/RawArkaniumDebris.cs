using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class RawArkaniumDebris: ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ExathiumSlag>();
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(gold: 60);
            Item.maxStack = 9999;
        }
    }
}
