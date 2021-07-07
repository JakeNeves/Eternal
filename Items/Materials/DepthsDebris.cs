using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class DepthsDebris : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debris of the Depths");
            Tooltip.SetDefault("'A Cluster of Crumbled Grimstone'");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.buyPrice(silver: 50);
            item.rare = ItemRarityID.Green;
            item.maxStack = 999;
        }
    }
}
