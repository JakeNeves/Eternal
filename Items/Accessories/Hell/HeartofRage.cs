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
            tooltips[0].overrideColor = new Color(180, 5, Main.DiscoR);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 5f;
        }
    }
}
