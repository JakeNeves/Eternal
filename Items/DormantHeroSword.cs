using Eternal.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class DormantHeroSword : ModItem
    {

        public override void SetStaticDefaults()
        {
             Tooltip.SetDefault("'Something seems to be missing here'\n[c/008060:Ancient Artifact]\nWhatever this is, it remains unknown...");
        }

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 62;
            item.rare = ItemRarityID.Gray;
        }
    }
}
