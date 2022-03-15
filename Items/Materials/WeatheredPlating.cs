using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class WeatheredPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weathered Plating");
            Tooltip.SetDefault("'It's weathered and well worn'");
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 36;
            item.rare = ItemRarityID.Red;
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
