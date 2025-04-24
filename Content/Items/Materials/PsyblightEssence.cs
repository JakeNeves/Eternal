using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class PsyblightEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 3));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 12);
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 9999;
        }
    }
}
