using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class NeoxCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("N30X Core");
            Tooltip.SetDefault("'The RGB gamer core, now with Chroma Support!'");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 40;
            item.value = Item.buyPrice(platinum: 2, gold: 50);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }
    }
}
