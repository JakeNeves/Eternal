using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class GhoulishGladius : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.damage = 440;
            item.knockBack = 3f;
            item.value = Item.sellPrice(gold: 3);
            item.useTime = 12;
            item.useAnimation = 12;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
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
