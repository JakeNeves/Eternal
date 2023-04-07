using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class GlacerWalkerChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The body of a Glacer Walker'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 30);
            Item.maxStack = 999;
        }
    }
}
