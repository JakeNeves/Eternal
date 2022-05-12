using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class KnifeHandle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("???");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 8;
            item.rare = ItemRarityID.Gray;
        }
    }
}
