using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class YoggieSpotch : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Holding it in your hands for too long could burn your flesh off'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
