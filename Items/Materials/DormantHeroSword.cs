using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class DormantHeroSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Something seems to be missing here'\n[c/008060:Ancient Artifact]\nWhatever this is, it remains unknown...");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 54;
            item.rare = ItemRarityID.Gray;
        }
    }
}
