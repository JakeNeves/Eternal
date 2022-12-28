using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Misc
{
    public class LetterofRecommendation : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Letter of Recommendation");
            Tooltip.SetDefault("Give this letter to the Emperor" +
                "\n'Signed by the emissary.'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Teal>();
        }
    }
}
