using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class GalaxianPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Durable plating from cosmic entities'");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 5, silver: 90);
            item.maxStack = 999;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }
    }
}
