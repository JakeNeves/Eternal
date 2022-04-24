using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Materials
{
    public class StarmetalBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A Cosmic Bar That is Very Sturdy and Durable...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 21;
            item.value = Item.buyPrice(gold: 15);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }
    }
}
