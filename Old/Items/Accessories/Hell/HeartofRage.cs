using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories.Hell
{
    class HeartofRage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Rage");
            Tooltip.SetDefault("Increased Melee Damage By 25%\nHell");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 0;
            item.rare = ItemRarityID.Red;
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
            player.meleeDamage += 0.25f;
        }
    }
}
