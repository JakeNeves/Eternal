﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Summon
{
    public class TripletsCore : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Core");
            Tooltip.SetDefault("Used at the Exo Beacon" +
                              "\nCalls Machines XE-68, XE-76 and XE-90");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
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