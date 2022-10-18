using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class EmperorsTrust : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor's Trust");
            Tooltip.SetDefault("Allows the emperor's Emissaries to settle in your town" +
                             "\nSome weapons receive special buffs when in your inventory" +
                             "\n'In the eyes of the emperor, as long as you have his trust within your reach, he will let his emissaries settle on your land.'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 30);
        }
    }
}
