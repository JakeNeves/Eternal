using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials.Elementalblights
{
    public class NatureblightCrystal : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Crystalized essance of forest creatures'");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 34;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Blue;
            item.maxStack = 999;
        }
    }
}
