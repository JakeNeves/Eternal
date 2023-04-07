using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class StarmetalBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'A Cosmic Bar That is Very Sturdy and Durable...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 21;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
