using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace Eternal.Items
{
    public class GalaxianPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A durable plating, perfect for forging starmetal utilities...'");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 30));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 5, silver: 90);
            item.maxStack = 99;
        }
    }
}
