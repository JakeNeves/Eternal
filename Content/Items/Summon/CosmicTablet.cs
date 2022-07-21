using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Summon
{
    public class CosmicTablet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used at the Altar of Cosmic Desire" +
                             "\nSummons the Cosmic Emperor" +
                             "\n'Used to worship the emperor, many times to your heart's desire!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 52;
            Item.rare = ModContent.RarityType<Magenta>();
        }
    }
}
