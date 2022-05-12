using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class OmnicronCore : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Core");
            Tooltip.SetDefault("Used at the Exo Beacon" +
                              "\nCalls Machine XM-2024");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 26;
            item.value = Item.sellPrice(platinum: 1, gold: 5);
            item.rare = ItemRarityID.Red;
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
