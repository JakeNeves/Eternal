using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class DroxCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used to craft Strong Weapons" +
                "\n'A Mysterious Core from Drox-Like Machines'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
            item.width = 12;
            item.height = 8;
            item.value = Item.sellPrice(silver: 10, copper: 30);
        }
    }
}
