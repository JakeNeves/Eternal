using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class KnifeBlade : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("???");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 10;
            item.rare = ItemRarityID.Gray;
        }
    }
}
