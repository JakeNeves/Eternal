using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class CosmoniumFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 6));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = Item.buyPrice(platinum: 1, gold: 25, silver: 50);
            Item.rare = ModContent.RarityType<Aquamarine>();
            Item.maxStack = 9999;
        }
    }
}
