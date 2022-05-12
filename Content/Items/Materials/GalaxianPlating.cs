using Eternal.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class GalaxianPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Durable plating from cosmic entities'");
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.maxStack = 999;
        }
    }
}
