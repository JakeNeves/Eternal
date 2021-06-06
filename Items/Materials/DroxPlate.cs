using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Materials
{
    public class DroxPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mischevious Plating");
            Tooltip.SetDefault("Used to craft Strong Weapons" +
                "\n'Mysterious Plating from Drox-Like Machines'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
            item.width = 40;
            item.height = 32;
            item.value = Item.sellPrice(silver: 10, copper: 30);
        }
    }
}
