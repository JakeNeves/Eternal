using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    class ValarantKnives : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valarant Knives [WIP]");
            Tooltip.SetDefault("They Don't Do Anything Yet...");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Red;
        }
    }
}
