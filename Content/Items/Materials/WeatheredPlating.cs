using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class WeatheredPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'It's weathered and well worn'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 36;
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
