using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class MysteriousNote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Note");
            Tooltip.SetDefault("[c/FF0000:---ATTENTION READER!---]" +
                "\nI am hearing word that another emperor has arrived here, as well as one of their emissaries." +
                "\nPlease take note of my benefits of me being your ruler." +
                "\nPeace" +
                "\nProtection" +
                "\nTranquility" +
                "\nDo not fall victim to false hopes and such and do not take part of that emperor's doings!" +
                "\n-Your royal highness, the Cosmic Emperor");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ItemRarityID.Gray;
        }
    }
}
