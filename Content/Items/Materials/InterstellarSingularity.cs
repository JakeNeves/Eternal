using Eternal.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class InterstellarSingularity : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The Essance of Cosmic Beings'");
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 1);
            Item.maxStack = 999;
        }
    }
}
