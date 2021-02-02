using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories.Hell
{
    class HeartofRage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Rage");
            Tooltip.SetDefault("Increased Melee Damage By 5%\nHell Mode Drop");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 0;
            item.rare = ItemRarityID.Expert;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Hell;
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.5f;
        }
    }
}
