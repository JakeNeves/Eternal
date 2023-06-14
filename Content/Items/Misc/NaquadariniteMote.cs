using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;

namespace Eternal.Content.Items.Misc
{
    public class NaquadariniteMote : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }

        
    }

    internal class Naquadarinite : CustomCurrencySingleCoin
    {
        public Naquadarinite(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
        {
            this.CurrencyTextKey = CurrencyTextKey;
            CurrencyTextColor = Color.DarkSlateBlue;
        }
    }
}
