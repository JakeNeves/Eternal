using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class ExathiumSlag : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = Item.sellPrice(platinum: 20);
            Item.maxStack = 9999;
        }
    }
}
