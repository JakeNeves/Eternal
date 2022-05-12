using Eternal.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class Astragel : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Not actual gel'");
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;
            Item.value = Item.sellPrice(platinum: 1, gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
