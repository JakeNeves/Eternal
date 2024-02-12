using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class RawArkrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<RawExathium>();
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(gold: 90);
            Item.maxStack = 9999;
        }
    }
}
